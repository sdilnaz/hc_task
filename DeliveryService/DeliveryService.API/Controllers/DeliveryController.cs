using AutoMapper;
using DeliveryService.Application.Commands;
using DeliveryService.Application.Dtos;
using DeliveryService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers
{
    [Route("api/delivery")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeliveryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut("{id}/take")]
        public async Task<IActionResult> TakeInWork([FromRoute] int id, CancellationToken cancellationToken)
        {
            var dto = new TakeDeliveryRequestDto { Id = id };
            await _mediator.Send(new TakeDeliveryRequestCommand(dto.Id), cancellationToken);
            return Ok($"Delivery Request with id {id} is in work");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var deliveries = await _mediator.Send(new GetAllDeliveryRequestsQuery(pageNumber, pageSize), cancellationToken);
            var deliveryDtos = _mapper.Map<IEnumerable<DeliveryRequestDto>>(deliveries);
            return Ok(new { CurrentPage = pageNumber, Items = deliveryDtos });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var deliveryRequest = await _mediator.Send(new GetDeliveryRequestByIdQuery(id), cancellationToken);
            if (deliveryRequest != null)
            {
                var dto = _mapper.Map<DeliveryRequestDto>(deliveryRequest); 
                return Ok(dto);
            }
            else
            {
                return NotFound($"No delivery request found with id {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteDeliveryRequestCommand(id), cancellationToken);
            return Ok($"Delivery Request with id {id} is deleted");
        }
    }
}
