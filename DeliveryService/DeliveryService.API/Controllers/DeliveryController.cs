using AutoMapper;
using DeliveryService.Application.Commands;
using DeliveryService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers
{
    [Route("api/delivery")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public DeliveryController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPut("{id}/take")]
        public async Task<IActionResult> TakeInWork([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _sender.Send(new TakeDeliveryRequestCommand(id), cancellationToken);
            return Ok($"Delivery Request with id {id} is in work");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var deliveries = await _sender.Send(new GetAllDeliveryRequestsQuery(pageNumber, pageSize), cancellationToken);
            return Ok(new { CurrentPage = pageNumber, Items = deliveries });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var deliveryRequest = await _sender.Send(new GetDeliveryRequestByIdQuery(id), cancellationToken);
            if (deliveryRequest != null)
            {
                return Ok(deliveryRequest);
            }
            else
            {
                return NotFound($"No delivery request found with id {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteDeliveryRequestCommand(id), cancellationToken);
            return Ok($"Delivery Request with id {id} is deleted");
        }
    }
}
