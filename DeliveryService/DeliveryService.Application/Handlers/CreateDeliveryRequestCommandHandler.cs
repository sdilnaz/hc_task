using MediatR;
using DeliveryService.Core.Interfaces;
using DeliveryService.Application.Commands;
using DeliveryService.Core.Models;
using AutoMapper;

namespace DeliveryService.Application.Handlers
{
    public class CreateDeliveryRequestCommandHandler : IRequestHandler<CreateDeliveryRequestCommand, Unit>
    {
        private readonly IDeliveryCreateRepository _repository;

        private readonly IMapper _mapper;

        public CreateDeliveryRequestCommandHandler(IDeliveryCreateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateDeliveryRequestCommand request, CancellationToken cancellationToken)
        {
            var orderId = request.Order.Id;

            var existingRequest = await _repository.GetByOrderIdAsync(orderId, cancellationToken);
            if (existingRequest != null) return Unit.Value; 

            var deliveryRequest = _mapper.Map<DeliveryRequest>(request.Order);

            await _repository.AddAsync(deliveryRequest, cancellationToken);
            return Unit.Value;
        }
    }
}
