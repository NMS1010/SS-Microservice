using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Category.Queries
{
    public class GetCategoryBySlugQuery : IRequest<CategoryDto>
    {
        public string Slug { get; set; }
    }

    public class GetCategoryBySlugHandler : IRequestHandler<GetCategoryBySlugQuery, CategoryDto>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryBySlugHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDto> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetCategoryBySlug(request);
        }
    }
}
