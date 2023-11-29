using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Validators.Category
{
	public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
	{
		public UpdateCategoryRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Slug).NotEmpty().NotNull();
			RuleFor(x => x.Status).NotNull();
		}
	}
}