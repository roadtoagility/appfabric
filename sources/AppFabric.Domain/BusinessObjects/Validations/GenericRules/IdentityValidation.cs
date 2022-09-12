using System;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Validation;
using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.GenericRules
{
    public class IdentityValidation : ValidationRule<BaseEntity<EntityId>>
    {
        private readonly Failure _invalidIdentityFailure;

        public IdentityValidation()
        {
            _invalidIdentityFailure = Failure.For("InvalidIdentity", "Invalid identity");
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