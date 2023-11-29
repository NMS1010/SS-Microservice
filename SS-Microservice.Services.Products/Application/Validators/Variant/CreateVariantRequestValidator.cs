using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Application.Validators.Variant
{
	public class CreateVariantRequestValidator : AbstractValidator<CreateVariantRequest>
	{
		public CreateVariantRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Sku).NotEmpty().NotNull();
			RuleFor(x => x.Quantity).NotEmpty().NotNull();
			RuleFor(x => x.ItemPrice).NotEmpty().NotNull();
			RuleFor(x => x.TotalPrice).NotEmpty().NotNull();
		}
	}
}