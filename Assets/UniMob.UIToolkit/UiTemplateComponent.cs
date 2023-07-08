using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public abstract class UiTemplateComponent : UiComponent
    {
        private readonly VisualTreeAsset _template;

        protected UiTemplateComponent(VisualTreeAsset template)
        {
            _template = template;
        }
        
        public sealed override void Create(VisualElement root)
        {
            _template.CloneTree(root);
        }
    }
}