using DeliveryService.Core.Models;
using DeliveryService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Infrastructure.Data;

namespace DeliveryService.Infrastructure.Persistence
{
    public class DeliveryRequestRepository : IDeliveryRequestRepository
    {
        private readonly ApplicationDBContext _context;

        public DeliveryRequestRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<DeliveryRequest?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.DeliveryRequests.FindAsync([id], cancellationToken);
        }

        public async Task<List<DeliveryRequest>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.DeliveryRequests
                .OrderBy(d => d.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(DeliveryRequest deliveryRequest, CancellationToken cancellationToken)
        {
            await _context.DeliveryRequests.AddAsync(deliveryRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(DeliveryRequest deliveryRequest, CancellationToken cancellationToken)
        {
            _context.DeliveryRequests.Update(deliveryRequest);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(DeliveryRequest deliveryRequest, CancellationToken cancellationToken)
        {
            _context.DeliveryRequests.Remove(deliveryRequest);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
