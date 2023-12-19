using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Commands.Address;

namespace SS_Microservice.Services.Address.Infrastructure.Consumers.Commands.Auth
{
    public class CreateAddressCommandConsumer : IConsumer<ICreateAddressCommand>
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CreateAddressCommandConsumer(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ICreateAddressCommand> context)
        {
            var command = _mapper.Map<Application.Features.Address.Commands.CreateAddressCommand>(context.Message);

            await _sender.Send(command);
        }
    }

}
