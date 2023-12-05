using Logic.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessagingLayer
{
    public class RMQBackgroundService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "hello";

        private readonly IServiceProvider _serviceProvider;

        public RMQBackgroundService(IServiceProvider serviceProvider)
        {
            // Initialize your RabbitMQ connection and channel
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Project manager for Crud operations
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            while (!stoppingToken.IsCancellationRequested)
            {
                consumer.Received += async (model, ea) =>
                {
                    // Handle the received message
                    byte[] body = ea.Body.ToArray();
                    string? message = Encoding.UTF8.GetString(body);
                    await CreateProject();
                    Console.WriteLine($"Received message: {message}");
                };

                _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            }
            return Task.CompletedTask;
        }

        private async Task CreateProject()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<IProjectManager>();
            }
        }
    }
}
