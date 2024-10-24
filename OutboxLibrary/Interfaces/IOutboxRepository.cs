using OutboxLibrary.Models;
namespace OutboxLibrary.Interfaces
{
    public interface IOutboxRepository
    {
        Task AddOutboxMessageAsync(OutboxMessage message, CancellationToken cancellationToken);
        Task<List<OutboxMessage>> GetUnprocessedOutboxMessagesAsync(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}