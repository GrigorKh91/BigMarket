namespace BigMarket.Services.EmailAPI.Messaging.AzureServiceBus
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
