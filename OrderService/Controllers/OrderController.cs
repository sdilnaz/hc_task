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
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();
            var orderDtos = orders.Select(s => s.ToOrderDto());
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound("No order record found with id {id}");
            }
            return Ok(order.ToOrderDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto orderDto)
        {
            try{
                var orderModel = orderDto.ToOrderFromCreateDTO();
                await _context.Orders.AddAsync(orderModel);
                await _context.SaveChangesAsync();

                var orderCreatedEvent = orderModel.ToOrderCreatedEvent();
                await _publishEndpoint.Publish(orderCreatedEvent);

                return CreatedAtAction(nameof(GetById), new { id = orderModel.Id }, orderModel.ToOrderDto());
            }
            catch(Exception ex){
                _logger.LogError($"Error creating order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }   
           }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderRequestDto updateDto)
        {
            var orderModel = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (orderModel == null)
            {
                return NotFound("No order record found with id {id}");
            }
            orderModel.ProductName = updateDto.ProductName;
            orderModel.Quantity = updateDto.Quantity;
            orderModel.Price = updateDto.Price;

            await _context.SaveChangesAsync();
            return Ok(orderModel.ToOrderDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var orderModel = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orderModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}