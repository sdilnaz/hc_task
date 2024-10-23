using Microsoft.EntityFrameworkCore;
using OrderService.Core.Interfaces;
using OrderService.Core.Models;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Presistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            return await _context.Orders.ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Orders.FindAsync(id, cancellationToken);
        }

        public async Task AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteOrderAsync(Order order, CancellationToken cancellationToken)
        {
            _context.Orders.Remove(order);
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
