using MediatR;

namespace DeliveryService.Application.Commands
{
    public record TakeDeliveryRequestCommand(int Id) : IRequest;
}
