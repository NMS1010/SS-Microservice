using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Features.Category.Queries
{
    public class GetAllCategoryQuery : GetCategoryPagingRequest, IRequest<PaginatedResult<CategoryDto>>
    {
    }

    public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, PaginatedResult<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<PaginatedResult<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllCategory(request);
        }
    }
}