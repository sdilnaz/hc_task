using DeliveryService.Core.Models;

namespace DeliveryService.Core.Interfaces
{
    public interface IDeliveryRequestRepository
    {
        Task<DeliveryRequest?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<DeliveryRequest>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task AddAsync(DeliveryRequest deliveryRequest, CancellationToken cancellationToken);
        Task UpdateAsync(DeliveryRequest deliveryRequest, CancellationToken cancellationToken);
        Task DeleteAsync(DeliveryRequest deliveryRequest, CancellationToken cancellationToken);
    
    }
}