using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.ListCategory.Commands
{
    public class DeleteListCategoryCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }
    public class DeleteListCategoryHandler : IRequestHandler<DeleteListCategoryCommand, bool>
    {
        private readonly ICategoryService _categoryService;

        public DeleteListCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<bool> Handle(DeleteListCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.DeleteListCategory(request);
        }
    }
}
