using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit; 
using DeliveryService.Models;
using SharedModels.Events;
using DeliveryService.Data;

namespace DeliveryService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
         private readonly ApplicationDBContext _context;

        public OrderCreatedConsumer(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var deliveryRequest = new DeliveryRequest
            {
                OrderId = context.Message.Id
            };

            await _context.DeliveryRequests.AddAsync(deliveryRequest);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Delivery Request Created for OrderID : {context.Message.Id}");
       
        }
    }
}