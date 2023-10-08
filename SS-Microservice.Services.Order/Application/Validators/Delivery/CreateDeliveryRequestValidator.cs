using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace SS_Microservice.Services.Order.Application.Validators.Delivery
{
    public class CreateDeliveryRequestValidator : AbstractValidator<CreateDeliveryRequest>
    {
        public CreateDeliveryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Image).NotEmpty();
        }
    }
}