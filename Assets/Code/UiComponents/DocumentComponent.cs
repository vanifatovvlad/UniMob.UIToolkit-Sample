using Code.Store;
using UniMob.UIToolkit;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class DocumentComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;
        private readonly DocumentPage _page;

        public DocumentComponent(VisualTreeAsset template, ViewStore viewStore, DocumentPage page) : base(template)
        {
            _viewStore = viewStore;
            _page = page;
        }

        public override void Init(VisualElement root)
        {
            root.Q<Label>("name").Render(Lifetime, () => _page.Info.Name);
            root.Q<Button>("back-button").clickable.OnClick(Lifetime, () => _viewStore.ShowOverview());
            
            //TODO render load status
        }
    }
}