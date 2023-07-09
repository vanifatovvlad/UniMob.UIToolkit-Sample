using Code.Store;
using UniMob.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class AppComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;
        private readonly Router _router;

        public AppComponent(ViewStore viewStore, Router router)
            : base(Resources.Load<VisualTreeAsset>("App"))
        {
            _viewStore = viewStore;
            _router = router;
        }

        public override void Init(VisualElement root)
        {
            var routeInput = root.Q<TextField>("route-input");

            root.Q("app-content").Render(Lifetime, () => BuildContent());
            root.Q<Label>("user-name").Render(Lifetime, () => _viewStore.IsAuthenticated ? _viewStore.CurrentUser.Value.Name : "unknown user");
            routeInput.Render(Lifetime, () => _viewStore.CurrentPath);
            root.Q<Button>("route-go-button").OnClick(Lifetime, () => _router.Go(routeInput.text));
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
            return new OverviewComponent(_viewStore, overviewPage);
        }

        private UiComponent BuildDocument(DocumentPage documentPage)
        {
            if (!_viewStore.IsAuthenticated)
            {
                return new LoginComponent(_viewStore, afterLogin: () => _viewStore.ShowDocument(documentPage.DocumentId));
            }

            return new DocumentComponent(_viewStore, documentPage);
        }
    }
}