using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Types.Model.Paging
{
    public static class PaginatedExtension
    {
        public static Task<PaginatedResult<T>> PaginatedListAsync<T>(this IQueryable<T> queryable, int pageIndex, int pageSize) =>
            PaginatedResult<T>.CreatePaginatedList(queryable, pageIndex, pageSize);

        public static Task<PaginatedResult<T>> PaginatedListMongoAsync<T>(this IFindFluent<T, T> queryable, int pageIndex, int pageSize) =>
            PaginatedResult<T>.CreatePaginatedListMongo(queryable, pageIndex, pageSize);
    }
}