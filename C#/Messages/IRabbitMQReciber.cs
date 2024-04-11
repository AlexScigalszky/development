using System.Threading;
using System.Threading.Tasks;

namespace Example.Messages
{
    public interface IRabbitMQReciber
    {
        public delegate void NewMessageHandler(string data);
        public event NewMessageHandler OnNewMessage;

        public Task StartListening(string queueName, CancellationToken stoppingToken);
    }
}
