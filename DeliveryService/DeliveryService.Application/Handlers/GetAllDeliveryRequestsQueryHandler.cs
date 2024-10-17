using DeliveryService.Application.Queries;
using DeliveryService.Core.Models;
using DeliveryService.Core.Interfaces;
using MediatR;

namespace DeliveryService.Application.Handlers
{
    public class GetAllDeliveryRequestsQueryHandler : IRequestHandler<GetAllDeliveryRequestsQuery, List<DeliveryRequest>>
    {
        private readonly IDeliveryRequestRepository _repository;

        public GetAllDeliveryRequestsQueryHandler(IDeliveryRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DeliveryRequest>> Handle(GetAllDeliveryRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
