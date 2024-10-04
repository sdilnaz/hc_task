using task1.Dtos.Order;
using task1.Models;

namespace task1.Mappers
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