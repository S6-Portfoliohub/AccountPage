using Logic.Managers;
using MessagingLayer.Helper;
using MessagingLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessagingLayer
{
    public class RMQBackgroundService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channelCreate;
        private readonly IModel _channelDelete;
        private readonly string _queueNameCreate = "hello";
        private readonly string _queueDelete = "delete";

        private readonly IServiceProvider _serviceProvider;

        public RMQBackgroundService(IServiceProvider serviceProvider, IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            // Initialize your RabbitMQ connection and channel
            ConnectionFactory factory = new ConnectionFactory() { HostName = rabbitMqSettings.Value.HostName, UserName = rabbitMqSettings.Value.UserName, Password = rabbitMqSettings.Value.Password};
            _connection = factory.CreateConnection();
            _channelCreate = _connection.CreateModel();
            _channelDelete = _connection.CreateModel();

            _channelCreate.QueueDeclare(queue: _queueNameCreate, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channelDelete.QueueDeclare(queue: _queueDelete, durable: false, exclusive: false, autoDelete: false, arguments: null);
            

            // Project manager for Crud operations
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channelCreate);
            consumer.Received += async (model, ea) =>
            {
                // Handle the received message
                byte[] body = ea.Body.ToArray();
                string? message = Encoding.UTF8.GetString(body);

                //create new project
                await CreateProject(SerializeMessagesHelper.SerializeProject(message));
                Console.WriteLine($"Received message: {message}");
            };

            var consumerDelete = new EventingBasicConsumer(_channelDelete);
            consumerDelete.Received += async (model, ea) =>
            {
                // Handle the received message
                byte[] body = ea.Body.ToArray();
                string? message = Encoding.UTF8.GetString(body);
                message = message.Replace("\"", "");

                //delete user
                await DeleteUser(message);
                Console.WriteLine($"Received message: {message}");
            };

            _channelCreate.BasicConsume(queue: _queueNameCreate, autoAck: true, consumer: consumer);
            _channelDelete.BasicConsume(queue: _queueDelete, autoAck: true, consumer: consumerDelete);
            
            return Task.CompletedTask;
        }

        private async Task CreateProject(ProjectMessageModel projectMessage)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IProjectManager? projectManager = scope.ServiceProvider.GetService<IProjectManager>();
                await projectManager.CreateProject(new() { Description = projectMessage.Description, Name = projectMessage.Name, UserId = projectMessage.UserID, Img = projectMessage.Img });
            }
        }

        private async Task DeleteUser(string userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IProjectManager? projectManager = scope.ServiceProvider.GetService<IProjectManager>();
                await projectManager.DeleteUser(userId);
            }
        }
    }
}
