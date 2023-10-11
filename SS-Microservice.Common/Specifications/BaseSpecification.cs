using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDescending(Expression<Func<T, object>> orderByDecending)
        {
            OrderByDescending = orderByDecending;
        }

        public void ApplyPaging(int take, int skip)
        {
            Take = take;
            Skip = skip;
            IsPagingEnabled = true;
        }
    }
}