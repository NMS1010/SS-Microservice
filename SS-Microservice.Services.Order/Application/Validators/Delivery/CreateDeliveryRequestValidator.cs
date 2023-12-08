using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace green_craze_be_v1.Application.Validators.Delivery
{
    public class CreateDeliveryRequestValidator : AbstractValidator<CreateDeliveryRequest>
    {
        public CreateDeliveryRequestValidator()
        {
            RuleFor(x => x.Price).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Image).NotEmpty().NotNull();
        }
    }
}