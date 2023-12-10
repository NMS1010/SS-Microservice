using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Events.User;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;

namespace SS_Microservice.Services.Basket.Application.Messaging.Events.User
{
    public class UserRegistedConsumer : IConsumer<IUserRegistedEvent>
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRegistedConsumer> _logger;

        public UserRegistedConsumer(ISender sender, IMapper mapper, ILogger<UserRegistedConsumer> logger)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IUserRegistedEvent> context)
        {
            var command = _mapper.Map<CreateBasketCommand>(context.Message);

            await _sender.Send(command);
        }
    }
}