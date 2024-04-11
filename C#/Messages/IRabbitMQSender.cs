namespace Example.Messages
{
    public interface IRabbitMQSender
    {
        void Send(string queue, string data);

    }
}
