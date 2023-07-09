using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public abstract class UiComponent : ILifetimeScope, IDisposable
    {
        private readonly LifetimeController _lifetimeController = new();

        public Lifetime Lifetime => _lifetimeController.Lifetime;

        public abstract void BuildTree([NotNull] VisualElement root);

        public abstract void Init([NotNull] VisualElement root);

        public virtual void Dispose()
        {
            _lifetimeController.Dispose();
        }
    }
}