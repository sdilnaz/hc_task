using Microsoft.EntityFrameworkCore;
using OutboxLibrary.Models;

public interface IOutboxDbContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
