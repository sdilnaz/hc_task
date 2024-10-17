using OrderService.Application.Dtos.Order;

namespace OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<OrderDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<OrderDto> CreateOrderAsync(CreateOrderRequestDto dto, CancellationToken cancellationToken);
        Task<OrderDto?> UpdateOrderAsync(int id, UpdateOrderRequestDto dto, CancellationToken cancellationToken);
        Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken);
    }
}