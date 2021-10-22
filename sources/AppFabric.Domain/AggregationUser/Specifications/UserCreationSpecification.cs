using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationUser.Specifications
{
    public class UserCreationSpecification : CompositeSpecification<User>
    {
        public override bool IsSatisfiedBy(User candidate)
        {
            throw new NotImplementedException();
        }
    }
}
