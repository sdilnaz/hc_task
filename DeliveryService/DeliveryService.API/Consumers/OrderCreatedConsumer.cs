using MassTransit;
using MediatR;
using SharedModels.Events;
using DeliveryService.Application.Commands;

namespace DeliveryService.API.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ISender _sender;
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ISender sender, ILogger<OrderCreatedConsumer> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderId = context.Message.Id;
            try
            {
                await _sender.Send(new CreateDeliveryRequestCommand(context.Message), context.CancellationToken);
                _logger.LogInformation("Delivery request created for OrderID: {orderId}", orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating delivery request for OrderID: {orderId}, Error: {ex}", orderId, ex.Message);
                throw;
            }
        }
    }
}
