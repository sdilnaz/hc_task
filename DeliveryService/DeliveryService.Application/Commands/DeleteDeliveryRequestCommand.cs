using MediatR;

namespace DeliveryService.Application.Commands
{
    public record DeleteDeliveryRequestCommand(int Id) : IRequest;
}
