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
    public class ProjectCreationSpecification : CompositeSpecification<Project>
    {
        List<ValidationRule<Project>> _rules;

        public ProjectCreationSpecification()
        {
            _rules = new List<ValidationRule<Project>>();
            _rules.Add(new ProjectNameValidation());
            _rules.Add(new ProjectBudgetDateValidation());
            _rules.Add(new ProjectOrderNumberValidation());
        }

        public override bool IsSatisfiedBy(Project candidate)
        {
            var isSatisfiedBy = _rules.All(rule => rule.IsValid(candidate));
            return isSatisfiedBy;
        }
    }
}
