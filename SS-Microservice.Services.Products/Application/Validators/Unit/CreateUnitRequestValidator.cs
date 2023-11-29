using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.Unit;

namespace SS_Microservice.Services.Products.Application.Validators.Unit
{
	public class CreateUnitRequestValidator : AbstractValidator<CreateUnitRequest>
	{
		public CreateUnitRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
		}
	}
}