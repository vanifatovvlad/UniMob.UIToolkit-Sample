using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniMob.UIToolkit
{
    public class UiComponentBuilder
    {
        private readonly VisualElement _root;

        private UiComponent _activeComponent;

        public UiComponentBuilder(Lifetime lifetime, VisualElement root)
        {
            _root = root;

            lifetime.Register(() => Build(null));
        }

        public void Build(UiComponent component)
        {
            while (_root.childCount > 0)
            {
                _root.RemoveAt(0);
            }

            if (_activeComponent != null)
            {
                _activeComponent.Dispose();
                Debug.Log("Dispose " + _activeComponent.GetType().Name);
            }

            _activeComponent = component;

            if (_activeComponent != null)
            {
                Debug.Log("Init " + _activeComponent.GetType().Name);

                _activeComponent?.Create(_root);
                _activeComponent?.Init(_root);
            }
        }
    }
}