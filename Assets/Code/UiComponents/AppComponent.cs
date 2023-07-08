using Code.Store;
using UniMob.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class AppComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;
        private readonly VisualTreeAsset _overviewTemplate;
        private readonly VisualTreeAsset _documentTemplate;

        public AppComponent(VisualTreeAsset template, ViewStore viewStore,
            VisualTreeAsset overviewTemplate, VisualTreeAsset documentTemplate) : base(template)
        {
            _viewStore = viewStore;
            _overviewTemplate = overviewTemplate;
            _documentTemplate = documentTemplate;
        }

        public override void Init(VisualElement root)
        {
            root.Q("app-content").Render(Lifetime, () => BuildContent());
            root.Q<Label>("user-name").Render(Lifetime, () => _viewStore.IsAuthenticated ? _viewStore.CurrentUser.Name : "unknown user");
            root.Q<TextField>("route-input").Render(Lifetime, () => _viewStore.CurrentPath);
            root.Q<Button>("route-go-button").clickable.OnClick(Lifetime, () => Debug.Log("CLICK!"));
        }

        private UiComponent BuildContent()
        {
            return _viewStore.CurrentPage switch
            {
                OverviewPage overviewPage => BuildOverview(overviewPage),
                DocumentPage documentPage => BuildDocument(documentPage),
                _ => null,
            };
        }

        private UiComponent BuildOverview(OverviewPage overviewPage)
        {
            return new OverviewComponent(_overviewTemplate, _viewStore, overviewPage);
        }

        private UiComponent BuildDocument(DocumentPage documentPage)
        {
            //TODO return Login if not authenticated
            //if (!_viewStore.IsAuthenticated)
            //{
            //    return new LoginComponent();
            //}

            return new DocumentComponent(_documentTemplate, _viewStore, documentPage);
        }
    }
}