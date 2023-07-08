using Code.Store;
using Code.UiComponents;
using UniMob;
using UniMob.UIToolkit;
using UnityEngine.UIElements;

namespace Code
{
    public class Main : LifetimeMonoBehaviour
    {
        public UIDocument uiDocument;
        public VisualTreeAsset appTreeAsset;
        public VisualTreeAsset overviewTreeAsset;
        public VisualTreeAsset documentTreeAsset;

        private UiComponentBuilder _appComponentBuilder;

        protected override void Start()
        {
            base.Start();

            var viewStore = new ViewStore(Lifetime);

            uiDocument.rootVisualElement.Render(Lifetime, BuildApp);

            viewStore.ShowOverview();

            // TODO: start router

            UiComponent BuildApp()
            {
                return new AppComponent(appTreeAsset, viewStore, overviewTreeAsset, documentTreeAsset);
            }
        }
    }
}