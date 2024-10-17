using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
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
    }
}
