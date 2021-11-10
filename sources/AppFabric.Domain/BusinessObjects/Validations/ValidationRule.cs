using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.BusinessObjects.Validations
{
    public abstract class ValidationRule<T>
    {
        public readonly bool VALID = true;
        public readonly bool NOT_VALID = false;
        public abstract bool IsValid(T candidate);
    }
}
