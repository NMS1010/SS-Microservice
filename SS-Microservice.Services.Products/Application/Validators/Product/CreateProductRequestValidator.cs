using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Validators.Product
{
	public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
	{
		public CreateProductRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.CategoryId).NotEmpty().NotNull();
			RuleFor(x => x.BrandId).NotEmpty().NotNull();
			RuleFor(x => x.UnitId).NotEmpty().NotNull();
			RuleFor(x => x.ShortDescription).NotEmpty().NotNull();
			RuleFor(x => x.Description).NotEmpty().NotNull();
			RuleFor(x => x.Code).NotEmpty().NotNull();
			RuleFor(x => x.Slug).NotEmpty().NotNull();
			RuleFor(x => x.ProductImages).NotEmpty().NotNull();
		}
	}
}