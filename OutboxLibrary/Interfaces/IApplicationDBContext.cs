using Microsoft.EntityFrameworkCore;
using OutboxLibrary.Models;

public interface IApplicationDbContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
