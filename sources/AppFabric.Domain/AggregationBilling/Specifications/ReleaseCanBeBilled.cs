using System.Linq;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationBilling.Specifications
{
    public class ReleaseCanBeBilled : CompositeSpecification<Billing>
    {
        private readonly EntityId _client;

        public ReleaseCanBeBilled(EntityId client)
        {
            _client = client;
        }

        public override bool IsSatisfiedBy(Billing candidate)
        {
            var differentClients = candidate.Releases.Select(x => x.ClientId.Equals(_client)).Count();
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