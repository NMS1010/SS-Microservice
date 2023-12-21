using AutoMapper;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Common.Constants;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Statistic.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications.Order;
using SS_Microservice.Services.Order.Application.Specifications.Transaction;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStatisticRepository _statisticRepository;

        public StatisticService(IUnitOfWork unitOfWork, IMapper mapper, IStatisticRepository statisticRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _statisticRepository = statisticRepository;
        }

        public async Task<List<StatisticOrderStatusDto>> GetStatisticOrderStatus(GetStatisticOrderStatusQuery query)
        {
            var resp = new List<StatisticOrderStatusDto>();
            foreach (var status in ORDER_STATUS.Status)
            {
                var count = await _unitOfWork.Repository<Domain.Entities.Order>()
                    .CountAsync(new OrderSpecification(query.StartDate, query.EndDate, status));
                resp.Add(new StatisticOrderStatusDto()
                {
                    Name = status,
                    Value = count
                });
            }

            return resp;
        }

        public async Task<List<StatisticRevenueDto>> GetStatisticRevenue(GetStatisticRevenueQuery query)
        {
            List<StatisticRevenueDto> resp = new();
            for (int month = 1; month <= 12; month++)
            {
                DateTime firstDate = new(query.Year, month, 1);
                int lastDay = DateTime.DaysInMonth(query.Year, month);
                DateTime lastDate = new(query.Year, month, lastDay);

                var transactions = await _unitOfWork.Repository<Transaction>()
                    .ListAsync(new TransactionSpecification(firstDate, lastDate));
                decimal revenue = 0;
                transactions.ForEach(transaction => revenue += transaction.TotalPay);

                resp.Add(new StatisticRevenueDto()
                {
                    Date = "Tháng " + month,
                    Revenue = revenue,
                });
            }

            return resp;
        }

        public async Task<List<StatisticTopSellingProductDto>> GetStatisticTopSellingProduct(GetStatisticTopSellingProductQuery query)
        {
            return await _statisticRepository.GetStatisticTopSellingProduct(query);
        }

        public async Task<List<StatisticTopSellingProductYearDto>> GetStatisticTopSellingProductYear(GetStatisticTopSellingProductYearQuery query)
        {
            List<StatisticTopSellingProductYearDto> resp = new();

            for (int month = 1; month <= 12; month++)
            {
                DateTime firstDate = new(query.Year, month, 1);
                int lastDay = DateTime.DaysInMonth(query.Year, month);
                DateTime lastDate = new(query.Year, month, lastDay);
                Dictionary<string, long> data = new();
                foreach (var product in query.Products)
                {
                    long sold = 0;
                    foreach (var variant in product.Variants)
                    {
                        var orderItems = await _unitOfWork.Repository<OrderItem>()
                            .ListAsync(new OrderItemSpecification(variant.Id, firstDate, lastDate, ORDER_STATUS.DELIVERED));

                        orderItems.ForEach(orderItem => sold += orderItem.Quantity * variant.Quantity);
                    }
                    data[product.Name] = sold;
                }
                resp.Add(new StatisticTopSellingProductYearDto()
                {
                    Date = "Tháng " + month,
                    Products = data,
                });
            }

            return resp;
        }

        public async Task<StatisticTotalDto> GetStatisticTotal(GetStatisticTotalQuery query)
        {
            var transactions = await _unitOfWork.Repository<Transaction>().ListAsync(new TransactionSpecification());
            decimal revenue = 0;
            transactions.ForEach(transaction => revenue += transaction.TotalPay);

            var orders = await _unitOfWork.Repository<Domain.Entities.Order>().CountAsync(new OrderSpecification(ORDER_STATUS.DELIVERED));

            return new StatisticTotalDto()
            {
                Orders = orders,
                Revenue = revenue,
            };
        }

        public async Task<List<StatisticTransactionDto>> GetTopLatestTransaction(GetStatisticTopLatestTransactionQuery query)
        {
            List<StatisticTransactionDto> transactionDtos = new();
            var transactions = await _unitOfWork.Repository<Transaction>().ListAsync(new TransactionSpecification(query.Top));
            foreach (var transaction in transactions)
            {
                var transactionDto = _mapper.Map<TransactionDto>(transaction);
                transactionDtos.Add(new StatisticTransactionDto()
                {
                    Transaction = transactionDto,
                    UserId = transaction.Order.UserId,
                });
            }
            return transactionDtos;
        }
    }
}
