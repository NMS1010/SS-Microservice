using SS_Microservice.Common.Specifications;

namespace SS_Microservice.Services.Products.Application.Specification.ProductImage
{
	public class ProductImageSpecification : BaseSpecification<Domain.Entities.ProductImage>
	{
		public ProductImageSpecification(long productId) : base(x => x.Product.Id == productId)
		{
			AddInclude(x => x.Product);
		}

		public ProductImageSpecification(long productId, bool isDefault = true)
			: base(x => x.Product.Id == productId && x.IsDefault == true) { }
	}
}