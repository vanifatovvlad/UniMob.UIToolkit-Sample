using Code.Store;
using UniMob.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class AppComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;

        public AppComponent(ViewStore viewStore) 
            : base(Resources.Load<VisualTreeAsset>("App"))
        {
            _viewStore = viewStore;
        }

        public override void Init(VisualElement root)
        {
            root.Q("app-content").Render(Lifetime, () => BuildContent());
            root.Q<Label>("user-name").Render(Lifetime, () => _viewStore.IsAuthenticated ? _viewStore.CurrentUser.Name : "unknown user");
            root.Q<TextField>("route-input").Render(Lifetime, () => _viewStore.CurrentPath);
            root.Q<Button>("route-go-button").OnClick(Lifetime, () => Debug.Log("CLICK!"));
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
            //TODO return Login if not authenticated
            //if (!_viewStore.IsAuthenticated)
            //{
            //    return new LoginComponent();
            //}

            return new DocumentComponent(_viewStore, documentPage);
        }
    }
}