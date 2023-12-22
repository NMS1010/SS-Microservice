namespace SS_Microservice.Common.Types.Model.Paging
{
    public static class PaginatedExtension
    {
        public static Task<PaginatedResult<T>> PaginatedListAsync<T>(this IQueryable<T> queryable, int pageIndex, int pageSize) =>
            PaginatedResult<T>.CreatePaginatedList(queryable, pageIndex, pageSize);
    }
}