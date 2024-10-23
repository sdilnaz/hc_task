namespace OrderService.Core.Interfaces
{
    public interface IOutboxService
    {
        Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken);
    }
}