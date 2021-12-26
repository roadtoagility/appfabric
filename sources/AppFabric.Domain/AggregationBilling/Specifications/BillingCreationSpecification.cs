using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationBilling.Specifications
{
    public class BillingCreationSpecification : CompositeSpecification<Billing>
    {
        public override bool IsSatisfiedBy(Billing candidate)
        {
            //TODO: criar validações de criação
            return true;
        }
    }
}