using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Dtos.Order;
using OrderService.Mappers;

namespace OrderService.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ApplicationDBContext context, IPublishEndpoint publishEndpoint, ILogger<OrderController> Logger)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
            _logger = Logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.ToListAsync(cancellationToken);
            var orderDtos = orders.Select(s => s.ToOrderDto());
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(id, cancellationToken);
            if (order == null)
            {
                return NotFound($"No order record found with id {id}");
            }
            return Ok(order.ToOrderDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto orderDto, CancellationToken cancellationToken)
        {
            
            var orderModel = orderDto.ToOrderFromCreateDTO();
            await _context.Orders.AddAsync(orderModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var orderCreatedEvent = orderModel.ToOrderCreatedEvent();
            await _publishEndpoint.Publish(orderCreatedEvent);

            return CreatedAtAction(nameof(GetById), new { id = orderModel.Id }, orderModel.ToOrderDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderRequestDto updateDto, CancellationToken cancellationToken)
        {
            var orderModel = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (orderModel == null)
            {
                return NotFound($"No order record found with id {id}");
            }
            orderModel.ProductName = updateDto.ProductName;
            orderModel.Quantity = updateDto.Quantity;
            orderModel.Price = updateDto.Price;

            await _context.SaveChangesAsync(cancellationToken);
            return Ok(orderModel.ToOrderDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var orderModel = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (orderModel == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orderModel);
            await _context.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}