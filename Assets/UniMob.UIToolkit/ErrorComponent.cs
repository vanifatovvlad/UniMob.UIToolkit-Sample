using System;
using UniMob.UIToolkit;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class ErrorComponent : UiComponent
    {
        private readonly Exception _ex;

        public ErrorComponent(Exception ex)
        {
            _ex = ex;
        }

        public override void BuildTree(VisualElement root)
        {
            root.Add(new Label {text = "Something went wrong"});
            root.Add(new Label {name = "error"});
        }

        public override void Init(VisualElement root)
        {
            root.Q<Label>("error").text = _ex.Message;
        }
    }
}