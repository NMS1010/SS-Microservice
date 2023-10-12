using MediatR;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Models.Address;

namespace SS_Microservice.Services.Address.Application.Features.Address.Commands
{
    public class CreateAddressCommand : CreateAddressRequest, IRequest<long>
    {
    }

    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, long>
    {
        private readonly IAddressService _addressService;

        public CreateAddressHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<long> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            return await _addressService.CreateAddress(request);
        }
    }
}