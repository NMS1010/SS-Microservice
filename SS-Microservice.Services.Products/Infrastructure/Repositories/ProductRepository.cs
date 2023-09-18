﻿using MongoDB.Driver;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Interfaces.Repositories;
using SS_Microservice.Services.Products.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace SS_Microservice.Services.Products.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> _dbSet;
        private readonly IProductContext _context;
        private readonly ICurrentUserService _currentUserService;

        public ProductRepository(IProductContext context, ICurrentUserService currentUserService) : base(context, currentUserService)
        {
            _dbSet = context.Database.GetCollection<Product>(typeof(Product).Name);
            _context = context;
            _currentUserService = currentUserService;
        }

        private static object GetSortColumn(Product p, string column)
        {
            if (string.IsNullOrEmpty(column))
                return p.Name;
            object res = column switch
            {
                "Description" => p.Description,
                "Price" => p.Price,
                "Origin" => p.Origin,
                "Quantity" => p.Quantity,
                _ => p.Name,
            };

            return res;
        }

        private static IFindFluent<Product, Product> SortByName(bool isAscending, IFindFluent<Product, Product> products)
        {
            return (isAscending
                ? products.SortBy(x => x.Name)
                : products.SortByDescending(x => x.Name));
        }

        private static IFindFluent<Product, Product> SortByDescription(bool isAscending, IFindFluent<Product, Product> products)
        {
            return (isAscending
                ? products.SortBy(x => x.Description)
                : products.SortByDescending(x => x.Description));
        }

        private static IFindFluent<Product, Product> SortByQuantity(bool isAscending, IFindFluent<Product, Product> products)
        {
            return (isAscending
                ? products.SortBy(x => x.Quantity)
                : products.SortByDescending(x => x.Quantity));
        }

        private static IFindFluent<Product, Product> SortByPrice(bool isAscending, IFindFluent<Product, Product> products)
        {
            return (isAscending
                ? products.SortBy(x => x.Price)
                : products.SortByDescending(x => x.Price));
        }

        private static IFindFluent<Product, Product> SortProduct(string column, bool isAscending, IFindFluent<Product, Product> products)
        {
            return column switch
            {
                "Name" => SortByName(isAscending, products),
                "Description" => SortByDescription(isAscending, products),
                "Quantity" => SortByQuantity(isAscending, products),
                "Price" => SortByPrice(isAscending, products),
                _ => SortByName(isAscending, products)
            };
        }

        public async Task<PaginatedResult<Product>> FilterProduct(GetAllProductQuery query)
        {
            var res = _dbSet.Find(x => query.Keyword != null
                     ? x.Name.ToLower().Contains(query.Keyword.ToString().ToLower())
                         || x.Description.ToLower().Contains(query.Keyword.ToString().ToLower())
                         || x.Quantity.ToString().Contains(query.Keyword.ToString())
                         || x.Price.ToString().Contains(query.Keyword.ToString())
                         || x.Origin.ToLower().Contains(query.Keyword.ToString().ToLower())
                     : true);
            if (query.ColumnName != null)
                res = SortProduct(query.ColumnName, query.TypeSort == "ASC", res);

            return await res.PaginatedListMongoAsync((int)query.PageIndex, (int)query.PageSize);
        }

        public async Task<bool> UpdateProductQuantity(UpdateProductQuantityCommand command)
        {
            using (var session = await _context.Client.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    var now = DateTime.Now;
                    foreach (var item in command.Products)
                    {
                        var product = await _dbSet.Find(filter: g => g.Id == item.ProductId).SingleOrDefaultAsync();
                        product.Quantity += item.Quantity;
                        product.UpdatedDate = now;
                        product.UpdatedBy = _currentUserService?.UserId ?? "system";
                        _dbSet.ReplaceOne(filter: g => g.Id == product.Id, replacement: product);
                    }
                    await session.CommitTransactionAsync();
                    return true;
                }
                catch
                {
                    await session.AbortTransactionAsync();
                    return false;
                }
            }
        }
    }
}