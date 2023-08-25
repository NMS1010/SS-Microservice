using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Basket.Application.Dto
{
    public class BasketDto
    {
        public PaginatedResult<BasketItemDto> BasketItems { get; set; }
    }
}