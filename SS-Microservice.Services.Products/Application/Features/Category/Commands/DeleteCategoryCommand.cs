using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Category.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.DeleteCategory(request);
        }
    }
}