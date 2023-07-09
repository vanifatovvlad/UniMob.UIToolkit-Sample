using System;
using Code.Store;
using UniMob.UIToolkit;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class ErrorComponent : UiComponent
    {
        private readonly ViewStore _viewStore;
        private readonly Exception _ex;

        public ErrorComponent(ViewStore viewStore, Exception ex)
        {
            _viewStore = viewStore;
            _ex = ex;
        }

        public override void BuildTree(VisualElement root)
        {
            root.Add(new Label {text = "Something wend wrong"});
            root.Add(new Label {name = "error"});
            root.Add(new Button {name = "go-home-button", text = "Show Overview"});
        }

        public override void Init(VisualElement root)
        {
            root.Q<Label>("error").text = _ex.Message;
            root.Q<Button>("go-home-button").OnClick(Lifetime, () => _viewStore.ShowOverview());
        }
    }
}