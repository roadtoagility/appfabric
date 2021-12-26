using System;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

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