namespace AppFabric.Domain.BusinessObjects.Validations
{
    public abstract class ValidationRule<T>
    {
        protected bool Valid => true;
        protected bool NotValid => false;
        public abstract bool IsValid(T candidate);
    }
}