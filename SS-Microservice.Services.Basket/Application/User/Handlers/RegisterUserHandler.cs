using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.User;
using SS_Microservice.Services.Basket.Application.Basket.Commands;

namespace SS_Microservice.Services.Basket.Application.User.Handlers
{
    public class RegisterUserHandler : IConsumer<UserRegisted>
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserHandler> _logger;

        public RegisterUserHandler(ISender sender, IMapper mapper, ILogger<RegisterUserHandler> logger)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserRegisted> context)
        {
            var command = _mapper.Map<BasketCreateCommand>(context.Message);
            _logger.LogInformation($"Message with userId = {command.UserId} is received");

            await _sender.Send(command);
        }
    }
}