﻿using MassTransit;
using SS_Microservice.Contracts.Events.Order;

namespace SS_Microservice.Services.Infrastructure.Application.Messaging.Events.Order
{
    public class OrderCreationCompletedConsumer : IConsumer<OrderCreationCompletedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreationCompletedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}