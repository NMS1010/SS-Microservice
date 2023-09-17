using MediatR;
using SS_Microservice.Services.Products.Application.Message.Category.Commands;
using SS_Microservice.Services.Products.Core.Interfaces;

namespace SS_Microservice.Services.Products.Application.Message.Category.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.UpdateCategory(request);
        }
    }
}