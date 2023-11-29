using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.Brand;

namespace SS_Microservice.Services.Products.Application.Validators.Brand
{
	public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
	{
		public CreateBrandRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Code).NotEmpty().NotNull();
			RuleFor(x => x.Description).NotEmpty().NotNull();
			RuleFor(x => x.Image).NotEmpty().NotNull();
		}
	}
}