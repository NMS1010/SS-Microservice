using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Message.Category.Queries;
using SS_Microservice.Services.Products.Core.Interfaces;

namespace SS_Microservice.Services.Products.Application.Message.Category.Handlers
{
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, PaginatedResult<CategoryDTO>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<PaginatedResult<CategoryDTO>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllCategory(request);
        }
    }
}