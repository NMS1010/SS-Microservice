using MediatR;

namespace SS_Microservice.Services.Products.Application.Message.Category.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public string CategoryId { get; set; }
    }
}