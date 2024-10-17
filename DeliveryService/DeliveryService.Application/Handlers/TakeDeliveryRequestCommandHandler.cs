using DeliveryService.Application.Commands;
using DeliveryService.Core.Interfaces;
using DeliveryService.Core.Models;
using MediatR;

namespace DeliveryService.Application.Handlers
{
    public class TakeDeliveryRequestCommandHandler : IRequestHandler<TakeDeliveryRequestCommand>
    {
        private readonly IDeliveryRequestRepository _repository;

        public TakeDeliveryRequestCommandHandler(IDeliveryRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(TakeDeliveryRequestCommand request, CancellationToken cancellationToken)
        {
            var deliveryRequest = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (deliveryRequest == null) throw new KeyNotFoundException($"No delivery request found with id {request.Id}");

            deliveryRequest.Status = DeliveryStatus.TakenIntoWork;
            await _repository.UpdateAsync(deliveryRequest, cancellationToken);

            return Unit.Value;
        }
    }
}
