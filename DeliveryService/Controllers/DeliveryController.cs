using DeliveryService.Data;
using DeliveryService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Controllers
{
    [Route("api/delivery")]
    [ApiController]

    public class DeliveryController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public DeliveryController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPut("{id}/take")]
        public async Task<IActionResult> TakeInWork([FromRoute] int id, CancellationToken cancellationToken)
        {
            var deliveryRequest = await _context.DeliveryRequests.FindAsync(id);
            if (deliveryRequest == null)
            {
                return NotFound($"No delivery request found with id {id}");
            }
            deliveryRequest.Status = DeliveryStatus.TakenIntoWork;
            _context.DeliveryRequests.Update(deliveryRequest);
            await _context.SaveChangesAsync(cancellationToken);
            return Ok($"Delivery Request with id {id} is in work");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, CancellationToken cancellationToken)
        {
            if (paginationQuery.PageNumber <= 0) paginationQuery.PageNumber = 1;
            if (paginationQuery.PageSize <= 0) paginationQuery.PageSize = 10;

            var deliveries = await _context.DeliveryRequests
                .OrderBy(d => d.CreatedAt)
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync(cancellationToken);

            var response = new
            {
                CurrentPage = paginationQuery.PageNumber,
                Items = deliveries
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var deliveryRequest = await _context.DeliveryRequests.FindAsync(id, cancellationToken);
            if (deliveryRequest == null)
            {
                return NotFound($"No delivery request found with id {id}");
            }
            return Ok(deliveryRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var deliveryRequest = await _context.DeliveryRequests.FindAsync(id, cancellationToken);
            if (deliveryRequest == null)
            {
                return NotFound($"No delivery request found with id {id}");
            }
            _context.DeliveryRequests.Remove(deliveryRequest);
            await _context.SaveChangesAsync(cancellationToken);
            return Ok($"Delivery Request with id {id} is deleted");
        }
    }
}