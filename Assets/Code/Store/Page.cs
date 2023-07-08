using System;
using UniMob;

namespace Code.Store
{
    public abstract class Page : ILifetimeScope, IDisposable
    {
        private readonly LifetimeController _lifetimeController = new();

        public Lifetime Lifetime => _lifetimeController.Lifetime;

        public void Dispose()
        {
            _lifetimeController.Dispose();
        }
    }
}