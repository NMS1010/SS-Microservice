﻿using MassTransit;
using SS_Microservice.Common.Messages.Events.User;

namespace SS_Microservice.Services.Infrastructure.Application.Features.User.Consumer
{
    public class UserRegistedConsumer : IConsumer<UserRegistedEvent>
    {
        public Task Consume(ConsumeContext<UserRegistedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}