using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
	public class ProductDto : BaseAuditableEntity<long>
	{
		public string Name { get; set; }
		public CategoryDto Category { get; set; }
		public SaleDto Sale { get; set; }
		public BrandDto Brand { get; set; }
		public UnitDto Unit { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public string Code { get; set; }
		public long Quantity { get; set; }
		public long ActualInventory { get; set; }
		public long Sold { get; set; }
		public double Rating { get; set; }
		public List<ProductImageDto> Images { get; set; }
		public List<VariantDto> Variants { get; set; }
		public string Slug { get; set; }
		public decimal Cost { get; set; }
		public string Status { get; set; }
	}
}