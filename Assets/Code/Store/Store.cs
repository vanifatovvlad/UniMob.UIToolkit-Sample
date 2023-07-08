using UniMob;

namespace Code.Store
{
    public abstract class Store : ILifetimeScope
    {
        public Lifetime Lifetime { get; }

        protected Store(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}