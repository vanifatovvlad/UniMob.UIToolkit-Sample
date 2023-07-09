using Code.Store;
using UniMob.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class DocumentComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;
        private readonly DocumentPage _documentPage;

        public DocumentComponent(ViewStore viewStore, DocumentPage documentPage)
            : base(Resources.Load<VisualTreeAsset>("Document"))
        {
            _viewStore = viewStore;
            _documentPage = documentPage;
        }

        public override void Init(VisualElement root)
        {
            root.Q<Label>("name").Render(Lifetime, () => _documentPage.Document.TryGetValue(out var doc) ? doc.Name : "Loading...");
            root.Q<Label>("text").Render(Lifetime, () => _documentPage.Document.TryGetValue(out var doc) ? doc.Text : "Loading...");
            root.Q<Button>("back-button").OnClick(Lifetime, () => _viewStore.ShowOverview());
        }
    }
}