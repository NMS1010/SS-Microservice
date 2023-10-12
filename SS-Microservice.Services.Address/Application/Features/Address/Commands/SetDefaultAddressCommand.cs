using MediatR;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.Address.Commands
{
    public class SetDefaultAddressCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string UserId { get; set; }
    }

    public class SetDefaultAddressHandler : IRequestHandler<SetDefaultAddressCommand, bool>
    {
        private readonly IAddressService _addressService;

        public SetDefaultAddressHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<bool> Handle(SetDefaultAddressCommand request, CancellationToken cancellationToken)
        {
            return await _addressService.SetAddressDefault(request);
        }
    }
}