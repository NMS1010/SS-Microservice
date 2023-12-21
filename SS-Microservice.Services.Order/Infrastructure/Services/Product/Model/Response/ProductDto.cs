using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Services.Product.Model.Response
{
    public class ProductDto : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public List<VariantDto> Variants { get; set; }
    }
    public class VariantDto : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
