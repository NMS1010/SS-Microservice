using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Services.Order.Infrastructure.Services.UserOperation.Model.Request;
using SS_Microservice.Services.Order.Infrastructure.Services.UserOperation.Model.Response;

namespace SS_Microservice.Services.Order.Infrastructure.Services.UserOperation
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IUserOperationClientAPI
    {
        [Get("api/reviews/internal/order-review")]
        Task<CustomAPIResponse<OrderReviewDto>> GetOrderReview([Query] GetOrderReviewRequest request);
    }
}
