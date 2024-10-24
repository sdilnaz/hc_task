namespace OutboxLibrary.Interfaces
{
    public interface IOutboxService
    {
        Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken);
    }
}