using AutoMapper;
using OrderService.Core.Models;
using SharedModels.Events;
using System.Text.Json;

namespace OrderService.Application.Mappers
{
    public class OutboxProfile : Profile
    {
        public OutboxProfile()
        {
            CreateMap<OrderCreatedEvent, OutboxMessage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())  
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.Payload, 
                opt => opt.MapFrom(src => JsonSerializer.Serialize(src, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })))
                .ForMember(dest => dest.OccurredAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Processed, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ProcessedAt, opt => opt.Ignore());
        }
    }
}
