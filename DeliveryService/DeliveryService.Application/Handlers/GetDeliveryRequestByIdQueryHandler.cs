using DeliveryService.Application.Queries;
using DeliveryService.Core.Models;
using DeliveryService.Core.Interfaces;
using MediatR;

namespace DeliveryService.Application.Handlers
{
    public class GetDeliveryRequestByIdQueryHandler : IRequestHandler<GetDeliveryRequestByIdQuery, DeliveryRequest?>
    {
        private readonly IDeliveryRequestRepository _repository;

        public GetDeliveryRequestByIdQueryHandler(IDeliveryRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeliveryRequest?> Handle(GetDeliveryRequestByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
