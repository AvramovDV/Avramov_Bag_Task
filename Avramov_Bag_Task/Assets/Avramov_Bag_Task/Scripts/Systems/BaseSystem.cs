namespace Avramov.Bag
{
    public abstract class BaseSystem : ISystem
    {
        public bool IsActive {  get; private set; }

        public void Activate()
        {
            if (IsActive)
                return;

            IsActive = true;
            Activated();
        }

        public void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
            Deactivated();
        }

        protected abstract void Activated();
        protected abstract void Deactivated();
    }
}
