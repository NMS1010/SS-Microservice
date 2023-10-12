using FluentValidation;
using SS_Microservice.Services.Address.Application.Models.Address;

namespace SS_Microservice.Services.Address.Application.Validators.Address
{
    public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
    {
        public UpdateAddressRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().NotNull();
            RuleFor(x => x.Receiver).NotEmpty().NotNull();
            RuleFor(x => x.Street).NotEmpty().NotNull();
            RuleFor(x => x.ProvinceId).NotEmpty().NotNull();
            RuleFor(x => x.DistrictId).NotEmpty().NotNull();
            RuleFor(x => x.WardId).NotEmpty().NotNull();
        }
    }
}