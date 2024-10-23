using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Core.Interfaces;
using OrderService.Core.Models;
using SharedModels.Events;

namespace OrderService.Application.Services
{
    public class OutboxService : IOutboxService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OutboxService> _logger;

        public OutboxService(IOrderRepository orderRepository, ILogger<OutboxService> logger, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken)
        {
            var messages = await _orderRepository.GetUnprocessedOutboxMessagesAsync(cancellationToken);
            foreach (var message in messages)
            {
                try
                {
                    _logger.LogInformation("Publishing message: {MessageId}, Type: {Type}", message.Id, message.Type);
                    var eventMessage = DeserializeEvent(message);
                    await _publishEndpoint.Publish(eventMessage, cancellationToken);
                    message.Processed = true;
                    message.ProcessedAt = DateTime.UtcNow;
                    _logger.LogInformation("Message {MessageId} processed successfully at {ProcessedAt}.", message.Id, message.ProcessedAt);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error publishing message with ID {MessageId}", message.Id);
                }
            }
            await _orderRepository.SaveChangesAsync(cancellationToken);
        }

        private object DeserializeEvent(OutboxMessage message)
        {
            return message.Type switch
            {
                nameof(OrderCreatedEvent) => JsonSerializer.Deserialize<OrderCreatedEvent>(
                    message.Payload,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? throw new InvalidOperationException($"Failed to deserialize {message.Type}"),

                _ => throw new InvalidOperationException($"Unknown message type: {message.Type}")
            };
        }

    }
}