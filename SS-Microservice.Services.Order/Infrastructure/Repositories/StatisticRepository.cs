using Microsoft.EntityFrameworkCore;
using SS_Microservice.Services.Order.Application.Common.Constants;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Statistic.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Infrastructure.Data.DBContext;

namespace SS_Microservice.Services.Order.Infrastructure.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly OrderDbContext _context;

        public StatisticRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<List<StatisticTopSellingProductDto>> GetStatisticTopSellingProduct(GetStatisticTopSellingProductQuery request)
        {
            var orderItems = (await _context.OrderItems
                .Include(x => x.Order)
                .ThenInclude(x => x.Transaction)
                .Where(x => x.Order.Status == ORDER_STATUS.DELIVERED
                    && x.Order.Transaction.CompletedAt >= request.StartDate
                    && x.Order.Transaction.CompletedAt <= request.EndDate)
                .Take(request.Top)
                .ToListAsync());

            var res = new List<StatisticTopSellingProductDto>();
            Dictionary<string, long> productGroup = new();
            orderItems.ForEach(item =>
            {
                var product = request.Products
                    .FirstOrDefault(x => x.Variants.Any(g => g.Id == item.VariantId));

                var variant = product
                    .Variants.FirstOrDefault(x => x.Id == item.VariantId);

                var productName = product?.Name;

                if (!productGroup.ContainsKey(productName))
                {
                    productGroup.Add(productName, item.Quantity * variant.Quantity);
                }
                else
                {
                    productGroup[productName] = productGroup[productName] + item.Quantity * variant.Quantity;
                }
            });

            return productGroup.Select(x => new StatisticTopSellingProductDto()
            {
                Name = x.Key,
                Value = x.Value
            }).ToList();
        }
    }
}
