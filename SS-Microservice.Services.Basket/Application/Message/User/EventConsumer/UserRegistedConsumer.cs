using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.User;
using SS_Microservice.Services.Basket.Application.Message.Basket.Commands;

namespace SS_Microservice.Services.Basket.Application.Message.User.Handlers
{
    public class UserRegistedConsumer : IConsumer<UserRegistedEvent>
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

        public async Task Consume(ConsumeContext<UserRegistedEvent> context)
        {
            var command = _mapper.Map<CreateBasketCommand>(context.Message);
            _logger.LogInformation($"Message with userId = {command.UserId} is received");

            await _sender.Send(command);
        }
    }
}