using FluentValidation;
using SS_Microservice.Services.Products.Application.Common.Enums;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Validators.Product
{
	public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
	{
		public UpdateProductRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.CategoryId).NotEmpty().NotNull();
			RuleFor(x => x.BrandId).NotEmpty().NotNull();
			RuleFor(x => x.UnitId).NotEmpty().NotNull();
			RuleFor(x => x.ShortDescription).NotEmpty().NotNull();
			RuleFor(x => x.Description).NotEmpty().NotNull();
			RuleFor(x => x.Code).NotEmpty().NotNull();
			RuleFor(x => x.Slug).NotEmpty().NotNull();
			RuleFor(x => x.Status).NotEmpty().NotNull();
			RuleFor(x => x.Status)
				.Must(x => PRODUCT_STATUS.Status.Contains(x))
				.WithMessage("Unexpected product status");
		}
	}
}