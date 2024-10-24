using DeliveryService.Application.Dtos;
using MediatR;

namespace DeliveryService.Application.Queries
{
    public record GetDeliveryRequestByIdQuery(int Id) : IRequest<DeliveryRequestDto?>;
}
