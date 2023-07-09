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

            var viewStore = new ViewStore(Lifetime);

            uiDocument.rootVisualElement.Render(Lifetime, () => new AppComponent(viewStore));

            viewStore.ShowOverview();

            // TODO: start router
        }
    }
}