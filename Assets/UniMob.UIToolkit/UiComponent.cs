using System;
using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public abstract class UiComponent : ILifetimeScope, IDisposable
    {
        private readonly LifetimeController _lifetimeController = new();

        public Lifetime Lifetime => _lifetimeController.Lifetime;

        public abstract void Create(VisualElement root);

        public abstract void Init(VisualElement root);

        public virtual void Dispose()
        {
            _lifetimeController.Dispose();
        }
    }
}