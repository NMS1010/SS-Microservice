using SS_Microservice.Common.Specifications;

namespace SS_Microservice.Services.Basket.Application.Specifications
{
    public class BasketSpecification : BaseSpecification<Domain.Entities.Basket>
    {
        public BasketSpecification(string userId)
            : base(x => x.UserId == userId)
        {
        }
    }
}