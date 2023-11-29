using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.ProductImage;

namespace SS_Microservice.Services.Products.Application.Validators.ProductImage
{
	public class CreateProdcutImageRequestValidator : AbstractValidator<CreateProductImageRequest>
	{
		public CreateProdcutImageRequestValidator()
		{
			RuleFor(x => x.Image).NotEmpty().NotNull();
			RuleFor(x => x.ProductId).NotNull();
		}
	}
}