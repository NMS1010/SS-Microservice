using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.Sale;

namespace SS_Microservice.Services.Products.Application.Validators.Sale
{
	public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
	{
		public UpdateSaleRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Description).NotEmpty().NotNull();
			RuleFor(x => x.StartDate).NotEmpty().NotNull();
			RuleFor(x => x.EndDate).NotEmpty().NotNull();
			RuleFor(x => x.PromotionalPercent).NotEmpty().NotNull();
			RuleFor(x => x.Slug).NotEmpty().NotNull();
		}
	}
}