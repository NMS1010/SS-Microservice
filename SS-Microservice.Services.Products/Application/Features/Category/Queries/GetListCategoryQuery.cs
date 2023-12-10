using MediatR;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Features.Category.Queries
{
    public class GetListCategoryQuery : GetCategoryPagingRequest, IRequest<PaginatedResult<CategoryDto>>
    {
    }

    public class GetListCategoryHandler : IRequestHandler<GetListCategoryQuery, PaginatedResult<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;

        public GetListCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<PaginatedResult<CategoryDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetListCategory(request);
        }
    }
}