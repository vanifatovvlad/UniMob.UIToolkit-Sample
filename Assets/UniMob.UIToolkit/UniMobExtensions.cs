using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public static class UniMobExtensions
    {
        public static void Render<T>(this ListView listView, Lifetime lifetime, Func<IList<T>> sources, Func<T, UiComponent> func)
        {
            var sourcesAtom = Atom.Computed(lifetime, sources);

            listView.makeItem += () =>
            {
                var item = new VisualElement();
                item.userData = new UiComponentBuilder(lifetime, item);
                return item;
            };
            listView.bindItem += (element, i) =>
            {
                if (element.userData is UiComponentBuilder builder)
                {
                    using (Atom.NoWatch)
                    {
                        builder.Build(func.Invoke(sourcesAtom.Value[i]));
                    }
                }
            };
            listView.unbindItem += (element, i) =>
            {
                if (element.userData is UiComponentBuilder builder)
                {
                    using (Atom.NoWatch)
                    {
                        builder.Build(null);
                    }
                }
            };

            Atom.Reaction(lifetime, () => listView.itemsSource = (IList) sourcesAtom.Value);
            lifetime.Register(() => listView.itemsSource = Array.Empty<T>());
        }

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

        public static void OnClick(this Button button, Lifetime lifetime, Action callback)
        {
            if (lifetime.IsDisposed)
            {
                return;
            }

            lifetime.Register(() => button.clicked -= Call);
            button.clicked += Call;

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