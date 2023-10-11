using FluentValidation;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Validators.User
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.NewPassword).NotEmpty().NotNull();
            RuleFor(x => x.OldPassword).NotEmpty().NotNull();
            RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull();
        }
    }
}