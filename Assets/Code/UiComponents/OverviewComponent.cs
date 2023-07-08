using System.Collections;
using Code.Store;
using UniMob;
using UniMob.UIToolkit;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class OverviewComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;
        private readonly OverviewPage _page;

        public OverviewComponent(VisualTreeAsset template, ViewStore viewStore, OverviewPage page) : base(template)
        {
            _viewStore = viewStore;
            _page = page;
        }

        public override void Init(VisualElement root)
        {
            var documents = root.Q<ListView>("documents-list");

            documents.makeItem += () =>
            {
                var item = new VisualElement();
                item.Add(new Button() {name = "open-document-button"});
                return item;
            };
            documents.bindItem += (element, i) =>
            {
                var btn = element.Q<Button>("open-document-button");
                btn.Render(Lifetime, () => _page.Documents[i].Name);
                btn.clickable.OnClick(Lifetime, () => _viewStore.ShowDocument(i));
            };

            Atom.Reaction(Lifetime, () => (IList) _page.Documents, v => documents.itemsSource = v);
            
            //TODO render load status
        }
    }
}