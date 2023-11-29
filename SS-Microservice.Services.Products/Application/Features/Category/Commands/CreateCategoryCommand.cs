using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Features.Category.Commands
{
    public class CreateCategoryCommand : CreateCategoryRequest, IRequest<long>
    {
    }

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, long>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<long> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.CreateCategory(request);
        }
    }
}