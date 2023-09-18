using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Features.Category.Queries
{
    public class GetAllCategoryQuery : CategoryPagingRequest, IRequest<PaginatedResult<CategoryDTO>>
    {
    }
}