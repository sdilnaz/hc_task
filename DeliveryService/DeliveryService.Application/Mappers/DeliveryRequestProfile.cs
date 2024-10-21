using AutoMapper;
using DeliveryService.Application.Dtos;
using DeliveryService.Core.Models;
using SharedModels.Events;

namespace DeliveryService.Application.Mappers
{
    public class DeliveryRequestProfile : Profile
    {
        public DeliveryRequestProfile()
        {
            CreateMap<OrderCreatedEvent, DeliveryRequest>()
                .ForMember(dest => dest.OrderId, 
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, 
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, 
                    opt => opt.MapFrom(_ => DeliveryStatus.Created));

            CreateMap<DeliveryRequest, DeliveryRequestDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<DeliveryRequestDto, DeliveryRequest>();
        }
    }
}
