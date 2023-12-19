using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Commands.Address;

namespace SS_Microservice.Services.Address.Infrastructure.Consumers.Commands.Auth
{
    public class UpdateAddressCommandConsumer : IConsumer<IUpdateAddressCommand>
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public UpdateAddressCommandConsumer(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IUpdateAddressCommand> context)
        {
            var command = _mapper.Map<Application.Features.Address.Commands.UpdateAddressCommand>(context.Message);

            await _sender.Send(command);
        }
    }
}
