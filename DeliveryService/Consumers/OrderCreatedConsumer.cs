using MassTransit;
using DeliveryService.Models;
using SharedModels.Events;
using DeliveryService.Data;
using Microsoft.Extensions.Logging;
using DeliveryService.Mappers;

namespace DeliveryService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ApplicationDBContext context, ILogger<OrderCreatedConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var deliveryRequest = context.Message.ToDeliveryRequest();

            await _context.DeliveryRequests.AddAsync(deliveryRequest);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Delivery Request Created for OrderID: {context.Message.Id}");
        }
    }
}