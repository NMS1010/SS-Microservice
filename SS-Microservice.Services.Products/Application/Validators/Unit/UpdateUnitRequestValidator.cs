using FluentValidation;
using SS_Microservice.Services.Products.Application.Model.Unit;

namespace SS_Microservice.Services.Products.Application.Validators.Unit
{
	public class UpdateUnitRequestValidator : AbstractValidator<UpdateUnitRequest>
	{
		public UpdateUnitRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Status).NotNull();
		}
	}
}