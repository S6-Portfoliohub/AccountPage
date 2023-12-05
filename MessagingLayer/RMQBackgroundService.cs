using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace MessagingLayer
{
    public class RMQBackgroundService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "hello";
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
