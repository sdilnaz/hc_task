using DeliveryService.Core.Models;
using MediatR;

namespace DeliveryService.Application.Queries
{
    public record GetDeliveryRequestByIdQuery(int Id) : IRequest<DeliveryRequest?>;
}
