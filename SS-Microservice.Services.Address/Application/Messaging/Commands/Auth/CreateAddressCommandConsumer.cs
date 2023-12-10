using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Commands.Address;

namespace SS_Microservice.Services.Address.Application.Messaging.Commands.Auth
{
    public class CreateAddressCommand : ICreateAddressCommand
    {
        public string UserId { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public long ProvinceId { get; set; }
        public long DistrictId { get; set; }
        public long WardId { get; set; }
    }
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
            var command = _mapper.Map<Features.Address.Commands.CreateAddressCommand>(context.Message);

            await _sender.Send(command);
        }
    }

}
