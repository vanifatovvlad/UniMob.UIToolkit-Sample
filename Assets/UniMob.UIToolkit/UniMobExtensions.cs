using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public static class UniMobExtensions
    {
        public static void Render<T>([NotNull] this ListView listView, Lifetime lifetime, [NotNull] Func<IList<T>> sources, [NotNull] Func<T, UiComponent> func)
        {
            if (listView == null) throw new ArgumentNullException(nameof(listView));
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            if (func == null) throw new ArgumentNullException(nameof(func));

            var sourcesAtom = Atom.Computed(lifetime, sources);

            listView.makeItem += MakeItem;
            listView.destroyItem += DestroyItem;
            listView.bindItem += BindItem;
            listView.unbindItem += UnbindItem;

            lifetime.Register(Dispose);

            Atom.Reaction(lifetime, () => listView.itemsSource = (IList) sourcesAtom.Value);

            void Dispose()
            {
                listView.makeItem -= MakeItem;
                listView.destroyItem -= DestroyItem;
                listView.bindItem -= BindItem;
                listView.unbindItem -= UnbindItem;
                listView.itemsSource = Array.Empty<T>();
            }

            VisualElement MakeItem()
            {
                using var _ = Atom.NoWatch;

                var element = new VisualElement();
                element.userData = new UiComponentBuilder(lifetime, element);
                return element;
            }

            void DestroyItem(VisualElement element)
            {
                using var _ = Atom.NoWatch;

                if (element.userData is UiComponentBuilder builder)
                {
                    builder.Dispose();
                }
            }

            void BindItem(VisualElement element, int i)
            {
                using var _ = Atom.NoWatch;

                if (element.userData is UiComponentBuilder builder)
                {
                    builder.Build(func.Invoke(sourcesAtom.Value[i]));
                }
            }

            void UnbindItem(VisualElement element, int i)
            {
                using var _ = Atom.NoWatch;

                if (element.userData is UiComponentBuilder builder)
                {
                    builder.Build(null);
                }
            }
        }

        public static void Render([NotNull] this VisualElement root, Lifetime lifetime, [NotNull] Func<UiComponent> func)
        {
            if (root == null) throw new ArgumentNullException(nameof(root));
            if (func == null) throw new ArgumentNullException(nameof(func));

            Render(new UiComponentBuilder(lifetime, root), lifetime, func);
        }

        public static void Render([NotNull] this UiComponentBuilder builder, Lifetime lifetime, [NotNull] Func<UiComponent> func)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (func == null) throw new ArgumentNullException(nameof(func));

            Atom.Reaction(lifetime, func, v => builder.Build(v));
        }

        public static void Render([NotNull] this TextElement text, Lifetime lifetime, [NotNull] Func<string> pull)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (pull == null) throw new ArgumentNullException(nameof(pull));

            Atom.Reaction(lifetime, pull, v => text.text = v ?? string.Empty);
        }

        public static void Render([NotNull] this TextField textField, Lifetime lifetime, [NotNull] Func<string> pull)
        {
            if (textField == null) throw new ArgumentNullException(nameof(textField));
            if (pull == null) throw new ArgumentNullException(nameof(pull));

            Atom.Reaction(lifetime, pull, v => textField.value = v ?? string.Empty);
        }

        public static void OnChange([NotNull] this TextField textField, Lifetime lifetime, [NotNull] Action<string> callback)
        {
            if (textField == null) throw new ArgumentNullException(nameof(textField));
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            if (lifetime.IsDisposed)
            {
                return;
            }

            lifetime.Register(() => textField.UnregisterValueChangedCallback(Call));
            textField.RegisterValueChangedCallback(Call);

            void Call(ChangeEvent<string> evt)
            {
                using var _ = Atom.NoWatch;
                callback.Invoke(evt.newValue);
            }
        }

        public static void OnClick([NotNull] this Button button, Lifetime lifetime, [NotNull] Action callback)
        {
            if (button == null) throw new ArgumentNullException(nameof(button));
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            if (lifetime.IsDisposed)
            {
                return;
            }

            lifetime.Register(() => button.clicked -= Call);
            button.clicked += Call;

            void Call()
            {
                using var _ = Atom.NoWatch;
                callback.Invoke();
            }
        }
    }
}