using DeliveryService.Core.Models;

namespace DeliveryService.Core.Interfaces
{
    public interface IDeliveryCreateRepository
    {
        Task<DeliveryRequest?> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken);
        Task AddAsync(DeliveryRequest deliveryRequest, CancellationToken cancellationToken);
    }
}