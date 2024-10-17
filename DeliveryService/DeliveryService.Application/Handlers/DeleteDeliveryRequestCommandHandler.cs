using MediatR;
using DeliveryService.Core.Interfaces;
using DeliveryService.Application.Commands;

namespace DeliveryService.Application.Handlers
{
    public class DeleteDeliveryRequestCommandHandler : IRequestHandler<DeleteDeliveryRequestCommand>
    {
        private readonly IDeliveryRequestRepository _repository;

        public DeleteDeliveryRequestCommandHandler(IDeliveryRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteDeliveryRequestCommand request, CancellationToken cancellationToken)
        {
            var deliveryRequest = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (deliveryRequest == null)
            {
                throw new KeyNotFoundException($"No delivery request found with id {request.Id}");
            }

            await _repository.DeleteAsync(deliveryRequest, cancellationToken);

            return Unit.Value;
        }
    }
}
