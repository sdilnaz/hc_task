using OrderService.Core.Models;

namespace OrderService.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancellationToken);
        Task AddOrderAsync(Order order, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task DeleteOrderAsync(Order order, CancellationToken cancellationToken);
    }
}