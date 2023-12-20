using SS_Microservice.Common.Types.Entities;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Auth.Model.Response;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.UserOperation.Application.Dto
{
    public class ReviewDto : BaseAuditableEntity<long>
    {
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public long OrderItemId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public string Reply { get; set; }
        public bool Status { get; set; }
        public string VariantName { get; set; }
        public ProductDto Product { get; set; }
        public UserDto User { get; set; }
    }
}
