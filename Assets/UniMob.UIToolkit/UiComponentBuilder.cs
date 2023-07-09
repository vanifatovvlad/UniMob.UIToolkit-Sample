using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public class UiComponentBuilder : IDisposable
    {
        private readonly VisualElement _root;

        private UiComponent _activeComponent;
        private VisualTreeAsset _activeTemplate;

        public UiComponentBuilder(Lifetime lifetime, [NotNull] VisualElement root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root), "root must be not null");

            lifetime.Register(() => Build(null));
        }

        public void Dispose()
        {
            using var _ = Atom.NoWatch;

            _activeComponent?.Dispose();
            _activeComponent = null;

            _root.Clear();
        }

        public void Build([CanBeNull] UiComponent component)
        {
            using var _ = Atom.NoWatch;

            var template = component is UiTemplateComponent templateComponent ? templateComponent.Template : null;

            var requiresTreeRebuild = component != null &&
                                      (_activeTemplate == null || template == null || _activeTemplate != template);

            _activeComponent?.Dispose();

            if (requiresTreeRebuild)
            {
                _root.Clear();
            }

            _activeComponent = component;

            if (_activeComponent == null)
            {
                return;
            }

            if (requiresTreeRebuild)
            {
                _activeTemplate = template;
                _activeComponent?.BuildTree(_root);
            }

            _activeComponent?.Init(_root);
        }
    }
}