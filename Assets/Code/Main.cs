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

            _appComponentBuilder = new UiComponentBuilder(Lifetime, uiDocument.rootVisualElement);
            _appComponentBuilder.Build(new AppComponent(appTreeAsset, viewStore,
                overviewTreeAsset, documentTreeAsset));
            
            viewStore.ShowOverview();

            // TODO: start router
        }
    }
}