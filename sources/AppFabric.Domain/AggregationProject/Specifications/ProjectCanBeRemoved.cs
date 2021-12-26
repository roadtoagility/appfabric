using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.BusinessObjects.Validations.ProjectRules;
using DFlow.Domain.Specifications;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationProject.Specifications
{

    public class ProjectCanBeRemoved : CompositeSpecification<Project>
    {
        public override bool IsSatisfiedBy(Project candidate)
        {
            return candidate.Status.Equals(ProjectStatus.Default());
        }
    }
}
