using DeliveryService.Models;
using SharedModels.Events;

namespace DeliveryService.Mappers
{
    public static class DeliveryRequestMappers
    {
        public static DeliveryRequest ToDeliveryRequest(this OrderCreatedEvent orderCreatedEvent)
        {
            return new DeliveryRequest
            {
                OrderId = orderCreatedEvent.Id
            };
        }
    }
}