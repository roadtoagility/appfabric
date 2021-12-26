using System.Collections.Generic;
using System.Linq;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.BusinessObjects.Validations.ProjectRules;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationProject.Specifications
{
    public class ProjectSpecification : CompositeSpecification<Project>
    {
        private readonly List<ValidationRule<Project>> _rules;

        public ProjectSpecification()
        {
            _rules = new List<ValidationRule<Project>>();
            _rules.Add(new ProjectCodeValidation());
            _rules.Add(new ProjectNameValidation());
            _rules.Add(new ProjectStatusValidation());
            _rules.Add(new ProjectStartDateValidation());
            _rules.Add(new ProjectBudgetDateValidation());
            _rules.Add(new ProjectOwnerDateValidation());
            _rules.Add(new ProjectOrderNumberValidation());
        }

        public override bool IsSatisfiedBy(Project candidate)
        {
            var isSatisfiedBy = _rules.All(rule => rule.IsValid(candidate));
            return isSatisfiedBy;
        }
    }
}