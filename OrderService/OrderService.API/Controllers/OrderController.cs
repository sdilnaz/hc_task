using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Dtos.Order;
using OrderService.Application.Interfaces;

namespace OrderService.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllAsync(cancellationToken);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByIdAsync(id, cancellationToken);
            if (order == null) return NotFound($"No order record found with id {id}");
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto orderDto, CancellationToken cancellationToken)
        {
            var createdOrder = await _orderService.CreateOrderAsync(orderDto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderRequestDto updateDto, CancellationToken cancellationToken)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(id, updateDto, cancellationToken);
            if (updatedOrder == null) return NotFound($"No order record found with id {id}");
            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _orderService.DeleteOrderAsync(id, cancellationToken);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
