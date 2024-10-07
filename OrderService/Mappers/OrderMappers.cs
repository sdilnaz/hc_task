using OrderService.Dtos.Order;
using OrderService.Models;

namespace OrderService.Mappers
{
    public static class OrderMappers
    {
        public static OrderDto ToOrderDto(this Order orderModel)
        {
            return new OrderDto
            {
                Id = orderModel.Id,
                ProductName = orderModel.ProductName,
                Quantity = orderModel.Quantity,
                Price = orderModel.Price
            };
        }

        public static Order ToOrderFromCreateDTO(this CreateOrderRequestDto orderDto)
        {
            return new Order
            {
                ProductName = orderDto.ProductName,
                Quantity = orderDto.Quantity,
                Price = orderDto.Price
                
            };
        }
    }
}