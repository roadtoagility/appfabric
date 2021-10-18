using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
