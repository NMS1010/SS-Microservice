using AutoMapper;
using MassTransit;
using SS_Microservice.Contracts.Events.User;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Infrastructure.Consumers.Events.User
{
    public class CreateBasketCommand
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
    public class UserRegistedConsumer : IConsumer<IUserRegistedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;

        public UserRegistedConsumer(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IUserRegistedEvent> context)
        {
            var command = _mapper.Map<CreateBasketCommand>(context.Message);

            await _basketService.CreateBasket(command);
        }
    }
}