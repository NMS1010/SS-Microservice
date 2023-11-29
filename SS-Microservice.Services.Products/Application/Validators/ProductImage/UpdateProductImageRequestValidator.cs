using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.ProductImage;

namespace SS_Microservice.Services.Products.Application.Validators.ProductImage
{
	public class UpdateProductImageRequestValidator : AbstractValidator<UpdateProductImageRequest>
	{
		public UpdateProductImageRequestValidator()
		{
			RuleFor(x => x.Image).NotEmpty().NotNull();
		}
	}
}