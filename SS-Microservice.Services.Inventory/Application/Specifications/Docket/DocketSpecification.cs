using SS_Microservice.Common.Specifications;

namespace SS_Microservice.Services.Inventory.Application.Specifications.Docket
{
    public class DocketSpecification : BaseSpecification<Domain.Entities.Docket>
    {
        public DocketSpecification(long productId) : base(x => x.ProductId == productId)
        {
        }


        public DocketSpecification(long orderId, bool status = true) : base(x => x.OrderId == orderId)
        {
        }

        public DocketSpecification(string type) : base(x => x.Type == type)
        {
        }

        public DocketSpecification(string type, DateTime firstDate, DateTime lastDate)
            : base(x => x.Type == type && x.CreatedAt >= firstDate && x.CreatedAt <= lastDate)
        {
        }
    }
}