using MediatR;
using SS_Microservice.Services.Products.Application.Dto;

namespace SS_Microservice.Services.Products.Application.Features.Category.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDTO>
    {
        public string CategoryId { get; set; }
    }
}