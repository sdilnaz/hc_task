using DeliveryService.Application.Dtos;
using DeliveryService.Core.Models;
using MediatR;

namespace DeliveryService.Application.Queries
{
    public record GetAllDeliveryRequestsQuery(int PageNumber, int PageSize) : IRequest<List<DeliveryRequestDto>>;
}