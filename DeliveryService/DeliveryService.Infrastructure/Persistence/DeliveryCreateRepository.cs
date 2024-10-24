using DeliveryService.Core.Models;
using DeliveryService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Infrastructure.Data;

namespace DeliveryService.Infrastructure.Persistence
{
    public class DeliveryCreateRepository : IDeliveryCreateRepository
    {
        private readonly ApplicationDBContext _context;

        public DeliveryCreateRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<DeliveryRequest?> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _context.DeliveryRequests
                .FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
        }

        public async Task AddAsync(DeliveryRequest deliveryRequest, CancellationToken cancellationToken)
        {
            await _context.DeliveryRequests.AddAsync(deliveryRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
