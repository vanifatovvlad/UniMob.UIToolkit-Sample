using Code.Domain;
using Code.Store;
using UniMob.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class OverviewItemComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;
        private readonly DocumentInfo _documentInfo;

        public OverviewItemComponent(ViewStore viewStore, DocumentInfo documentInfo)
            : base(Resources.Load<VisualTreeAsset>("OverviewItem"))
        {
            _viewStore = viewStore;
            _documentInfo = documentInfo;
        }

        public override void Init(VisualElement root)
        {
            root.Q<Button>("open-document-button").Render(Lifetime, () => _documentInfo.Name);
            root.Q<Button>("open-document-button").OnClick(Lifetime, () => _viewStore.ShowDocument(_documentInfo.ID));
        }
    }
}