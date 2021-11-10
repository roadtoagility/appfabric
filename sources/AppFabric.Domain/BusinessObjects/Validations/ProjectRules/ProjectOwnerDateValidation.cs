using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectOwnerDateValidation : ValidationRule<Project>
    {
        private ValidationFailure _ownerNullFailure;
        private ValidationFailure _ownerInvalidFailure;

        public ProjectOwnerDateValidation()
        {
            _ownerNullFailure = new ValidationFailure("Project.Owner", "Owner do projeto deve ser informado");
            _ownerInvalidFailure = new ValidationFailure("Project.Owner", "Endereço de email do Owner inválido");
        }

        public override bool IsValid(Project candidate)
        {
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
           + "@"
           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (candidate.Owner == null)
            {
                candidate.AppendValidationResult(_ownerNullFailure);
                return NOT_VALID;
            }
            else if (!regex.Match(candidate.Owner.Value).Success)
            {
                candidate.AppendValidationResult(_ownerInvalidFailure);
                return NOT_VALID;
            }

            return VALID;
        }
    }
}
