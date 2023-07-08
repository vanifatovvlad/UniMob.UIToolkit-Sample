using System;
using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public static class UniMobExtensions
    {
        public static void Render(this VisualElement root, Lifetime lifetime, Func<UiComponent> func)
        {
            Render(new UiComponentBuilder(lifetime, root), lifetime, func);
        }

        public static void Render(this UiComponentBuilder builder, Lifetime lifetime, Func<UiComponent> func)
        {
            Atom.Reaction(lifetime, func, v => builder.Build(v));
        }

        public static void Render(this TextElement text, Lifetime lifetime, Func<string> pull)
        {
            Atom.Reaction(lifetime, pull, v => text.text = v);
        }

        public static void Render(this TextField textField, Lifetime lifetime, Func<string> pull)
        {
            Atom.Reaction(lifetime, pull, v => textField.value = v);
        }

        public static void OnClick(this Clickable clickable, Lifetime lifetime, Action callback)
        {
            if (lifetime.IsDisposed)
            {
                return;
            }

            lifetime.Register(() => clickable.clicked -= Call);
            clickable.clicked += Call;

            void Call()
            {
                using (Atom.NoWatch)
                {
                    callback?.Invoke();
                }
            }
        }
    }
}