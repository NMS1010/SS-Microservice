using MediatR;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Features.Product.Commands
{
    public class UpdateProductImageCommand : ProductImageUpdateRequest, IRequest<bool>
    {
    }
}