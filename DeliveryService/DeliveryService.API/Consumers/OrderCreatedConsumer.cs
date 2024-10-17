using MassTransit;
using MediatR;
using SharedModels.Events;
using DeliveryService.Application.Commands;

namespace DeliveryService.API.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(IMediator mediator, ILogger<OrderCreatedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderId = context.Message.Id;
            try
            {
                await _mediator.Send(new CreateDeliveryRequestCommand(context.Message), context.CancellationToken);
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
