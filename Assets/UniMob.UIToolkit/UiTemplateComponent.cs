using System;
using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public abstract class UiTemplateComponent : UiComponent
    {
        public VisualTreeAsset Template { get; }

        protected UiTemplateComponent(VisualTreeAsset template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template), "template must be not null");
            }

            Template = template;
        }

        public sealed override void BuildTree(VisualElement root)
        {
            Template.CloneTree(root);
        }
    }
}