using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Interfaces.Repositories;
using SS_Microservice.Services.Products.Domain.Entities;
using SS_Microservice.Services.Products.Infrastructure.Repositories;

namespace SS_Microservice.Services.Categorys.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly IMongoCollection<Category> _dbSet;

        public CategoryRepository(IProductContext context, ICurrentUserService currentUserService) : base(context, currentUserService)
        {
            _dbSet = context.Database.GetCollection<Category>(typeof(Category).Name);
        }

        private static IFindFluent<Category, Category> SortByName(bool isAscending, IFindFluent<Category, Category> categories)
        {
            return (isAscending
                ? categories.SortBy(x => x.Name)
                : categories.SortByDescending(x => x.Name));
        }

        private static IFindFluent<Category, Category> SortByDescription(bool isAscending, IFindFluent<Category, Category> categories)
        {
            return (isAscending
                ? categories.SortBy(x => x.Description)
                : categories.SortByDescending(x => x.Description));
        }

        private static IFindFluent<Category, Category> SortCategory(string column, bool isAscending, IFindFluent<Category, Category> categories)
        {
            return column switch
            {
                "Name" => SortByName(isAscending, categories),
                "Description" => SortByDescription(isAscending, categories),
                _ => SortByName(isAscending, categories)
            };
        }

        public async Task<PaginatedResult<Category>> FilterCategory(GetAllCategoryQuery query)
        {
            var res = _dbSet.Find(x => query.Search == null
                        || x.Name.ToLower().Contains(query.Search.ToString().ToLower())
                        || x.Description.ToLower().Contains(query.Search.ToString().ToLower()));
            if (query.ColumnName != null)
                res = SortCategory(query.ColumnName, query.IsSortAccending, res);

            return await res.PaginatedListMongoAsync((int)query.PageIndex, (int)query.PageSize);
        }
    }
}