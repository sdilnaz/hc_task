using OutboxLibrary.Models;
using OutboxLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OutboxLibrary.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly IOutboxDbContext _context;

        public OutboxRepository(IOutboxDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddOutboxMessageAsync(OutboxMessage message, CancellationToken cancellationToken)
        {
            await _context.OutboxMessages.AddAsync(message, cancellationToken);
        }

        public async Task<List<OutboxMessage>> GetUnprocessedOutboxMessagesAsync(CancellationToken cancellationToken)
        {
            return await _context.OutboxMessages
                            .Where(m => m.Processed == false)
                            .OrderBy(m => m.OccurredAt)
                            .ToListAsync(cancellationToken);
        }
    }
}
