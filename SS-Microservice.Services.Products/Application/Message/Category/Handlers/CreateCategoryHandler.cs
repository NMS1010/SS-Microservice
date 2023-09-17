using MediatR;
using SS_Microservice.Services.Products.Application.Message.Category.Commands;
using SS_Microservice.Services.Products.Core.Interfaces;

namespace SS_Microservice.Services.Products.Application.Message.Category.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.AddCategory(request);
        }
    }
}