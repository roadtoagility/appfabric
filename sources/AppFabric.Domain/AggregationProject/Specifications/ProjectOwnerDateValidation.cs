using System.Text.RegularExpressions;
using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectOwnerDateValidation : ValidationRule<Project>
    {
        private readonly ValidationFailure _ownerInvalidFailure;
        private readonly ValidationFailure _ownerNullFailure;

        public ProjectOwnerDateValidation()
        {
            _ownerNullFailure = new ValidationFailure("Project.Owner", "Owner do projeto deve ser informado");
            _ownerInvalidFailure = new ValidationFailure("Project.Owner", "Endereço de email do Owner inválido");
        }

        public override bool IsValid(Project candidate)
        {
            var regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                  + "@"
                                  + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (candidate.Owner == null)
            {
                candidate.AppendValidationResult(_ownerNullFailure);
                return NotValid;
            }

            if (!regex.Match(candidate.Owner.Value).Success)
            {
                candidate.AppendValidationResult(_ownerInvalidFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}