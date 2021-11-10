using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.BusinessObjects;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.BusinessObjects.Validations.GenericRules
{

    public class IdentityValidation : ValidationRule<BaseEntity<EntityId>>
    {
        private ValidationFailure _invalidIdentityFailure;

        public IdentityValidation()
        {
            _invalidIdentityFailure = new ValidationFailure("InvalidIdentity", "Invalid identity");
        }

        public override bool IsValid(BaseEntity<EntityId> candidate)
        {
            if (candidate.Identity == null || candidate.Identity.Value == Guid.Empty)
            {
                candidate.AppendValidationResult(_invalidIdentityFailure);
                return NOT_VALID;
            }

            return VALID;
        }
    }
}
