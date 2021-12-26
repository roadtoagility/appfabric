using System;
using DFlow.Domain.BusinessObjects;
using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.GenericRules
{
    public class IdentityValidation : ValidationRule<BaseEntity<EntityId>>
    {
        private readonly ValidationFailure _invalidIdentityFailure;

        public IdentityValidation()
        {
            _invalidIdentityFailure = new ValidationFailure("InvalidIdentity", "Invalid identity");
        }

        public override bool IsValid(BaseEntity<EntityId> candidate)
        {
            if (candidate.Identity == null || candidate.Identity.Value == Guid.Empty)
            {
                candidate.AppendValidationResult(_invalidIdentityFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}