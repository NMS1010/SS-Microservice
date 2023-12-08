using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace SS_Microservice.Services.Order.Application.Validators.Delivery
{
    public class UpdateDeliveryRequestValidator : AbstractValidator<UpdateDeliveryRequest>
    {
        public UpdateDeliveryRequestValidator()
        {
            RuleFor(x => x.Price).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Status).NotNull();
        }
    }
}