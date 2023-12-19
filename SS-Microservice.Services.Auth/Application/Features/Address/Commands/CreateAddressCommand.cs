using SS_Microservice.Contracts.Commands.Address;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Features.Address.Commands
{
    public class CreateAddressCommand : CreateAddressRequest, ICreateAddressCommand
    {
        public Guid CorrelationId { get; set; }
    }
}
