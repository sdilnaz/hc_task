using OrderService.Application.Dtos.Order;
using OrderService.Application.Interfaces;
using AutoMapper;
using OrderService.Core.Models;
using SharedModels.Events;
using OrderService.Core.Interfaces;
using OutboxLibrary.Models;
using OutboxLibrary.Interfaces;

namespace OrderService.Application.Services
{
    public class OrderProcessingService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOutboxRepository _outboxRepository;
        private readonly IMapper _mapper;

        public OrderProcessingService(IOrderRepository orderRepository, IMapper mapper, IOutboxRepository outboxRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _outboxRepository = outboxRepository;
        }

        public async Task<List<OrderDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id, cancellationToken);
            if (order == null) return null;

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderRequestDto dto, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(dto);
            await _orderRepository.AddOrderAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            var orderCreatedEvent = _mapper.Map<OrderCreatedEvent>(order);
            var outboxMessage = _mapper.Map<OutboxMessage>(orderCreatedEvent);

            await _outboxRepository.AddOutboxMessageAsync(outboxMessage, cancellationToken);
            await _outboxRepository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> UpdateOrderAsync(int id, UpdateOrderRequestDto dto, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id, cancellationToken);
            if (order == null) return null;

            order.ProductName = dto.ProductName;
            order.Quantity = dto.Quantity;
            order.Price = dto.Price;

            _mapper.Map(dto, order);
            await _orderRepository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id, cancellationToken);
            if (order == null) return false;

            await _orderRepository.DeleteOrderAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
