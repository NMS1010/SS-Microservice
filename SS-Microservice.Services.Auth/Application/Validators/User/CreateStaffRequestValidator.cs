using FluentValidation;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Validators.User
{
    public class CreateStaffRequestValidator : AbstractValidator<CreateStaffRequest>
    {
        public CreateStaffRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Type).NotEmpty().NotNull();
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Avatar).NotEmpty().NotNull();
            RuleFor(x => x.Gender).NotEmpty().NotNull();
            RuleFor(x => x.Dob).NotEmpty().NotNull();
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
            RuleFor(x => x.Phone).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}