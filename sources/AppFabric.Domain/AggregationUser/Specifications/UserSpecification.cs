using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationUser.Specifications
{
    public class UserSpecification : CompositeSpecification<User>
    {
        public override bool IsSatisfiedBy(User candidate)
        {
            //TODO: escrevi e sai correndo
            var isValid = true;
            if (string.IsNullOrEmpty(candidate.Name.Value))
            {
                candidate.AppendValidationResult(new ValidationFailure("EmptyName",
                    "O nome do usuário não pode estar vazio."));
                isValid = false;
            }

            //TODO: alterar para CPF
            if (string.IsNullOrEmpty(candidate.Cnpj.Value))
            {
                candidate.AppendValidationResult(new ValidationFailure("EmptyCnpj",
                    "O cnpj do usuário não pode estar vazio."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(candidate.CommercialEmail.Value))
            {
                candidate.AppendValidationResult(new ValidationFailure("EmptyName",
                    "O email do usuário não pode estar vazio."));
                isValid = false;
            }
            else
            {
                var pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
                if (!Regex.IsMatch(candidate.CommercialEmail.Value, pattern))
                {
                    candidate.AppendValidationResult(new ValidationFailure("InvalidEmail",
                    "Endereço de e-mail inválido."));
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
