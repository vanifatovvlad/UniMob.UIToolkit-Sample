using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public class UiComponentBuilder
    {
        private readonly VisualElement _root;

        private UiComponent _activeComponent;
        private VisualTreeAsset _activeTemplate;

        public UiComponentBuilder(Lifetime lifetime, VisualElement root)
        {
            _root = root;

            lifetime.Register(() => Build(null));
        }

        public void Build(UiComponent component)
        {
            var template = component is UiTemplateComponent templateComponent ? templateComponent.Template : null;

            var requiresTreeRebuild = component != null &&
                                      (_activeTemplate == null || template == null || _activeTemplate != template);

            if (requiresTreeRebuild)
            {
                _root.Clear();
            }

            _activeComponent?.Dispose();
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