namespace CBV_SB_Shared.Messages
{
    public interface IRabbitMQSender
    {
        void Send(string queue, string data);

    }
}
