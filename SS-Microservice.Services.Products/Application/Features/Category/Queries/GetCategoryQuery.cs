using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Category.Queries
{
    public class GetCategoryQuery : IRequest<CategoryDto>
    {
        public long Id { get; set; }
    }

    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetCategory(request);
        }
    }
}