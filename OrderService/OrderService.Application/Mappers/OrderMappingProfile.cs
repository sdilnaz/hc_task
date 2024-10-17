using AutoMapper;
using OrderService.Application.Dtos.Order;
using OrderService.Core.Models;
using SharedModels.Events;

namespace OrderService.Application.Mappers
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDto>();

            CreateMap<CreateOrderRequestDto, Order>();

            CreateMap<Order, OrderCreatedEvent>()
                .ConstructUsing(order => new OrderCreatedEvent(
                    order.Id,
                    order.ProductName,
                    order.Quantity,
                    order.Price
                ));
        }
    }
}
