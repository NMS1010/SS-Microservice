using MediatR;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Message.Category.Commands
{
    public class CreateCategoryCommand : CategoryCreateRequest, IRequest<bool>
    {
    }
}