using MediatR;
using SharedModels.Events;

namespace DeliveryService.Application.Commands
{
    public record CreateDeliveryRequestCommand(OrderCreatedEvent Order) : IRequest<Unit>;
}
