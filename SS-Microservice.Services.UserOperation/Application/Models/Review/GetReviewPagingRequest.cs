using SS_Microservice.Common.Types.Model.Paging;

namespace SS_Microservice.Services.UserOperation.Application.Models.Review
{
    public class GetReviewPagingRequest : PagingRequest
    {
        public long? ProductId { get; set; }

        public long? Rating { get; set; }
    }
}