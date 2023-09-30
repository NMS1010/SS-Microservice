using MongoDB.Driver;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;
using SS_Microservice.Services.Products.Application.Interfaces.Repositories;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly IMongoCollection<Brand> _dbSet;

        public BrandRepository(IProductContext context, ICurrentUserService currentUserService) : base(context, currentUserService)
        {
            _dbSet = context.Database.GetCollection<Brand>(typeof(Brand).Name);
        }

        private static IFindFluent<Brand, Brand> SortByName(bool isAscending, IFindFluent<Brand, Brand> brands)
        {
            return (isAscending
                ? brands.SortBy(x => x.Name)
                : brands.SortByDescending(x => x.Name));
        }

        private static IFindFluent<Brand, Brand> SortByDescription(bool isAscending, IFindFluent<Brand, Brand> brands)
        {
            return (isAscending
                ? brands.SortBy(x => x.Description)
                : brands.SortByDescending(x => x.Description));
        }

        private static IFindFluent<Brand, Brand> SortByCode(bool isAscending, IFindFluent<Brand, Brand> brands)
        {
            return (isAscending
                ? brands.SortBy(x => x.Description)
                : brands.SortByDescending(x => x.Description));
        }

        private static IFindFluent<Brand, Brand> SortBrand(string column, bool isAscending, IFindFluent<Brand, Brand> brands)
        {
            return column switch
            {
                "Name" => SortByName(isAscending, brands),
                "Description" => SortByDescription(isAscending, brands),
                "Code" => SortByCode(isAscending, brands),
                _ => SortByName(isAscending, brands)
            };
        }

        public async Task<PaginatedResult<Brand>> FilterBrand(GetAllBrandQuery query)
        {
            var res = _dbSet.Find(x => query.Keyword == null
                        || x.Name.ToLower().Contains(query.Keyword.ToString().ToLower())
                        || x.Description.ToLower().Contains(query.Keyword.ToString().ToLower()));
            if (query.ColumnName != null)
                res = SortBrand(query.ColumnName, query.TypeSort == "ASC", res);

            return await res.PaginatedListMongoAsync((int)query.PageIndex, (int)query.PageSize);
        }
    }
}