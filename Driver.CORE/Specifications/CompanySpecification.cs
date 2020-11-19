using Driver.CORE.Entities;
using System;
using System.Linq.Expressions;

namespace Driver.CORE.Specifications
{
    public class CompanySpecification : BaseSpecification<Company>
    {
        public CompanySpecification(Expression<Func<Company, bool>> criteria) : base(criteria)
        {
        }

        public CompanySpecification(Guid id) : base(i => i.Id.Equals(id))
        {
        }
    }
}
