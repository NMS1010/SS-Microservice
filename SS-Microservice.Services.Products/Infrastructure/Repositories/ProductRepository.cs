using MongoDB.Driver;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Product.Queries;
using SS_Microservice.Services.Products.Core.Entities;
using System;
using System.Linq.Expressions;

namespace SS_Microservice.Services.Products.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> _dbSet;

        public ProductRepository(IProductContext context) : base(context)
        {
            _dbSet = context.Database.GetCollection<Product>(typeof(Product).Name);
        }

        private static IFindFluent<Product, Product> SortProduct(string column, bool isAscending, IFindFluent<Product, Product> products)
        {
            var tempFunc = column switch
            {
                "Description" => new Func<Product, object>(x => x.Description),
                "Price" => new Func<Product, object>(x => x.Price),
                "Origin" => new Func<Product, object>(x => x.Origin),
                "Quantity" => new Func<Product, object>(x => x.Quantity),
                _ => new Func<Product, object>(x => x.Name),
            };
            var expression = Expression.Lambda<Func<Product, object>>(Expression.Call(tempFunc.Method));
            return (isAscending
                ? products.SortBy(expression)
                : products.SortByDescending(expression));
        }

        public async Task<PaginatedResult<Product>> FilterProduct(ProductGetAllQuery query)
        {
            var res = _dbSet.Find(filter: x => x.Name.ToLower().Contains(query.Keyword.ToString().ToLower())
                     || x.Description.ToLower().Contains(query.Keyword.ToString().ToLower())
                     || x.Quantity.ToString().Contains(query.Keyword.ToString())
                     || x.Price.ToString().Contains(query.Keyword.ToString())
                     || x.Origin.ToLower().Contains(query.Keyword.ToString().ToLower()));

            return await SortProduct(query.ColumnName, query.TypeSort == "ASC", res).PaginatedListMongoAsync((int)query.PageIndex, (int)query.PageSize);
        }
    }
}