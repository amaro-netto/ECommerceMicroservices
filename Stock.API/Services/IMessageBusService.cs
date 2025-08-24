namespace Stock.API.Services
{
    public interface IMessageBusService
    {
        void PublishMessage(string message);
    }
}