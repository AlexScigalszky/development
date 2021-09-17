using System.Threading;
using System.Threading.Tasks;

namespace CBV_SB_Shared.Messages
{
    public interface IRabbitMQReciber
    {
        public delegate void NewMessageHandler(string data);
        public event NewMessageHandler OnNewMessage;

        public Task StartListening(string queueName, CancellationToken stoppingToken);
    }
}
