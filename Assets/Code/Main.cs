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

        protected override void Start()
        {
            base.Start();

            var fetcher = new Fetcher();
            var viewStore = new ViewStore(Lifetime, fetcher);
            var router = new Router(viewStore);

            uiDocument.rootVisualElement.Render(Lifetime, () => new AppComponent(viewStore, router));

            viewStore.ShowOverview();
        }
    }
}