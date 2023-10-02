using MediatR;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Models.Address;

namespace SS_Microservice.Services.Address.Application.Features.Address.Commands
{
    public class CreateAddressCommand : CreateAddressRequest, IRequest<bool>
    {
        public string UserId { get; set; }
    }

    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, bool>
    {
        private readonly IAddressService _addressService;

        public CreateAddressHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<bool> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            return await _addressService.CreateAddress(request);
        }
    }
}