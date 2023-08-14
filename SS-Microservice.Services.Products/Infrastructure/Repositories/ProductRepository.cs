using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IProductContext context) : base(context)
        {
        }
    }
}