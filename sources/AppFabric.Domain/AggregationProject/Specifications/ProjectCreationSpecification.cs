using AppFabric.Domain.BusinessObjects;
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
        public override bool IsSatisfiedBy(Project candidate)
        {
            var isSatisfiedBy = true;

            if (string.IsNullOrEmpty(candidate.Name.Value))
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Name",
                    "O nome do projeto não pode estar vazio"));
                isSatisfiedBy = false;
            }

            if(candidate.Budget == null)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Budget",
                    "Budget do projeto deve ser informado"));
                isSatisfiedBy = false;
            }
            else if(candidate.Budget.Value < Decimal.Zero)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Budget",
                    "Não é possível informar um budget com valor negativo"));
                isSatisfiedBy = false;
            }

            if (candidate.OrderNumber == null || string.IsNullOrEmpty(candidate.OrderNumber.Number))
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.OrderNumber",
                    "OS do projeto deve ser informada"));
                isSatisfiedBy = false;
            }else if (!candidate.OrderNumber.IsAproved)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.OrderNumber",
                    "A ordem de serviço precisa estar aprovada"));
                isSatisfiedBy = false;
                //TODO: se esquecer de setar o valor do isSatisfiedBy, da ruim, code smell
            }

            return isSatisfiedBy;
        }
    }
}
