using System.Collections.Generic;
using System.Linq;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.BusinessObjects.Validations.ProjectRules;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationProject.Specifications
{
    public class ProjectCreationSpecification : CompositeSpecification<Project>
    {
        public override bool IsSatisfiedBy(Project candidate)
        {
            return (candidate.IsValid && candidate.Status.Equals(ProjectStatus.Default()));
        }
    }
}