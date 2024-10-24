using DeliveryService.Application.Queries;
using DeliveryService.Application.Dtos; 
using DeliveryService.Core.Models;
using DeliveryService.Core.Interfaces;
using MediatR;
using AutoMapper;

namespace DeliveryService.Application.Handlers
{
    public class GetAllDeliveryRequestsQueryHandler : IRequestHandler<GetAllDeliveryRequestsQuery, List<DeliveryRequestDto>>
    {
        private readonly IDeliveryRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDeliveryRequestsQueryHandler(IDeliveryRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DeliveryRequestDto>> Handle(GetAllDeliveryRequestsQuery request, CancellationToken cancellationToken)
        {
            var deliveries =  await _repository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);
            return _mapper.Map<List<DeliveryRequestDto>>(deliveries);
        }
    }
}


