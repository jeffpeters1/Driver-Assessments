using Driver.CORE.Entities;
using System;
using System.Linq.Expressions;

namespace Driver.CORE.Specifications
{
    public class AssessmentSpecification : BaseSpecification<Assessment>
    {
        public AssessmentSpecification(Expression<Func<Assessment, bool>> criteria) : base(criteria)
        {
            AddIncludes();
        }

        public AssessmentSpecification(Guid id) : base(i => i.Id.Equals(id))
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(x => x.Driver);
        }
    }
}
