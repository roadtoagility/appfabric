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
    public class ProjectSpecification : CompositeSpecification<Project>
    {
        public override bool IsSatisfiedBy(Project candidate)
        {
            var isSatisfiedBy = true;
            if (string.IsNullOrEmpty(candidate.Code.Value))
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Code",
                    "O código do projeto não pode estar vazio"));
                isSatisfiedBy = false;
            }

            if (string.IsNullOrEmpty(candidate.Name.Value))
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Name",
                    "O nome do projeto não pode estar vazio"));
                isSatisfiedBy = false;
            }

            if (candidate.Status == null)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Status",
                    "O nome do projeto não pode estar nulo"));
                isSatisfiedBy = false;
            }
            else if (!Enumerable.Range(0, 2).Contains(candidate.Status.Value))
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Status",
                    "Status inválido para o projeto"));
                isSatisfiedBy = false;
            }

            if (candidate.StartDate == null)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.StartDate",
                    "Data não pode ser nula"));
                isSatisfiedBy = false;
            }

            if (candidate.StartDate.Value >= DateTime.UnixEpoch)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.StartDate",
                    "Data de inicio inválida"));
                isSatisfiedBy = false;
            }

            if (candidate.Budget == null)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Budget",
                    "Budget do projeto deve ser informado"));
                isSatisfiedBy = false;
            }
            else if (candidate.Budget.Value < Decimal.Zero)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Budget",
                    "Não é possível informar um budget com valor negativo"));
                isSatisfiedBy = false;
            }

            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (candidate.Owner == null)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Owner",
                    "Owner do projeto deve ser informado"));
                isSatisfiedBy = false;
            }
            else if (!regex.Match(candidate.Owner.Value).Success)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.Owner",
                       "Endereço de email do Owner inválido"));

            }

            if (candidate.OrderNumber == null || string.IsNullOrEmpty(candidate.OrderNumber.Number))
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.OrderNumber",
                    "OS do projeto deve ser informada"));
                isSatisfiedBy = false;
            }
            else if (!candidate.OrderNumber.IsAproved)
            {
                candidate.AppendValidationResult(new ValidationFailure("Project.OrderNumber",
                    "A ordem de serviço precisa estar aprovada"));
            }

            return isSatisfiedBy;
        }
    }
}
