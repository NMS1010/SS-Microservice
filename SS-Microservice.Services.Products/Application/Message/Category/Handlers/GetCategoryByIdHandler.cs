using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Message.Category.Queries;
using SS_Microservice.Services.Products.Core.Interfaces;

namespace SS_Microservice.Services.Products.Application.Message.Category.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryByIdHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetCategoryById(request);
        }
    }
}