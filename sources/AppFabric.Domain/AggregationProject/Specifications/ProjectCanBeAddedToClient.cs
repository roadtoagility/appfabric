using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationProject.Specifications
{
    public class ProjectCanBeAddedToClient : CompositeSpecification<Project>
    {
        public override bool IsSatisfiedBy(Project candidate)
        {
            return !candidate.Status.Equals(ProjectStatus.Finished());
        }
    }
}