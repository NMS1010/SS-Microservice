using MediatR;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Models.Address;

namespace SS_Microservice.Services.Address.Application.Features.Address.Commands
{
    public class UpdateAddressCommand : UpdateAddressRequest, IRequest<bool>
    {
    }

    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, bool>
    {
        private readonly IAddressService _addressService;

        public UpdateAddressHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            return await _addressService.UpdateAddress(request);
        }
    }
}