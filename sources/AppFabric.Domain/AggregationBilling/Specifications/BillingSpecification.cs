using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationBilling.Specifications
{
    public class BillingSpecification : CompositeSpecification<Billing>
    {
        public override bool IsSatisfiedBy(Billing candidate)
        {
            //TODO: Como vai ficar a validação do Id? criar uma base
            //RuleFor(billing => billing.Id).SetValidator(new EntityIdValidator());
            var differentClients = candidate.Releases.Select(x => x.ClientId.Value).Distinct().Count();
            if (differentClients > 1)
            {
                candidate.AppendValidationResult(new ValidationFailure("ClientReleases",
                    "Só é possível faturar releases de um mesmo cliente"));
                return false;
            }

            return true;
        }
    }
}
