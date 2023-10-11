using FluentValidation;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Validators.Auth
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .NotNull();

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}