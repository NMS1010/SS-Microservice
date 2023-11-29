using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Validators.Category
{
	public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
	{
		public CreateCategoryRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Image).NotNull();
			RuleFor(x => x.Slug).NotEmpty().NotNull();
		}
	}
}