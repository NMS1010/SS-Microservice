using Microsoft.EntityFrameworkCore;

namespace SS_Microservice.Common.Types.Model.Paging
{
    public class PaginatedResult<T>
    {
        public int CurrentItemCount { get; set; }
        public int ItemsPerPage { get; set; }
        public long TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();

        public PaginatedResult(List<T> items, int pageIndex, long totalCount, int pageSize)
        {
            var totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items;
            PageIndex = pageIndex;
            TotalItems = totalCount;
            TotalPages = totalPage;
            ItemsPerPage = pageSize;
            CurrentItemCount = items.Count;
        }

        public static async Task<PaginatedResult<T>> CreatePaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<T>(items, pageIndex, count, pageSize);
        }
    }
}