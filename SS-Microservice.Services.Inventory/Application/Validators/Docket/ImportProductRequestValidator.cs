using FluentValidation;
using SS_Microservice.Services.Inventory.Application.Models.Inventory;

namespace SS_Microservice.Services.Inventory.Application.Validators.Docket
{
    public class ImportProductRequestValidator : AbstractValidator<ImportProductRequest>
    {
        public ImportProductRequestValidator()
        {
            RuleFor(x => x.Quantity).NotNull();
            RuleFor(x => x.ActualInventory).NotNull();
            RuleFor(x => x.ProductId).NotNull();
        }
    }
}
