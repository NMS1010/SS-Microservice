using MediatR;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Features.Category.Commands
{
    public class UpdateCategoryCommand : CategoryUpdateRequest, IRequest<bool>
    {
    }
}