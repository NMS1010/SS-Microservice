using MediatR;
using SS_Microservice.Services.Address.Application.Interfaces;

namespace SS_Microservice.Services.Address.Application.Features.Address.Commands
{
    public class DeleteAddressCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string UserId { get; set; }
    }

    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, bool>
    {
        private readonly IAddressService _addressService;

        public DeleteAddressHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            return await _addressService.DeleteAddress(request);
        }
    }
}