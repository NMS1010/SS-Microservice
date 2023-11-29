using FluentValidation;
using SS_Microservice.Services.Products.Application.Common.Enums;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Application.Validators.Variant
{
	public class UpdateVariantRequestValidator : AbstractValidator<UpdateVariantRequest>
	{
		public UpdateVariantRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Sku).NotEmpty().NotNull();
			RuleFor(x => x.Quantity).NotEmpty().NotNull();
			RuleFor(x => x.ItemPrice).NotEmpty().NotNull();
			RuleFor(x => x.TotalPrice).NotEmpty().NotNull();
			RuleFor(x => x.Status).NotEmpty().NotNull();
			RuleFor(x => x.Status)
				.Must(x => VARIANT_STATUS.Status.Contains(x))
				.WithMessage("Unexpected variant status");
		}
	}
}