using FluentValidation;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace green_craze_be_v1.Application.Validators.User
{
    public class UpdateStaffRequestValidator : AbstractValidator<UpdateStaffRequest>
    {
        public UpdateStaffRequestValidator()
        {
            RuleFor(x => x.Type).NotEmpty().NotNull();
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Gender).NotEmpty().NotNull();
            RuleFor(x => x.Dob).NotEmpty().NotNull();
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
            RuleFor(x => x.Phone).NotEmpty().NotNull();
        }
    }
}