using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Queries
{
    public class GetAllProductQuery : ProductPagingRequest, IRequest<PaginatedResult<ProductDTO>>
    {
    }
}