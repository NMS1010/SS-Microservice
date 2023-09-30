using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Products.Application.Model.Variant
{
    public class GetVariantPagingRequest : PagingRequest
    {
        public string ProductId { get; set; }
    }
}