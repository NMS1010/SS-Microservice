using SS_Microservice.Services.Order.Infrastructure.Services.Auth.Model.Response;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class StatisticTransactionDto
    {
        public TransactionDto Transaction { get; set; }
        public UserDto User { get; set; }
        public string UserId { get; set; }

    }
}
