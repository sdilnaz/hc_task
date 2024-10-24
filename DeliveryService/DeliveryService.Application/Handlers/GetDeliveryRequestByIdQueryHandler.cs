using DeliveryService.Application.Queries;
using DeliveryService.Core.Models;
using DeliveryService.Core.Interfaces;
using MediatR;
using DeliveryService.Application.Dtos;
using AutoMapper;

namespace DeliveryService.Application.Handlers
{
    public class GetDeliveryRequestByIdQueryHandler : IRequestHandler<GetDeliveryRequestByIdQuery, DeliveryRequestDto?>
    {
        private readonly IDeliveryRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetDeliveryRequestByIdQueryHandler(IDeliveryRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DeliveryRequestDto?> Handle(GetDeliveryRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var deliveryRequest = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<DeliveryRequestDto?>(deliveryRequest);
       
        }
    }
}
