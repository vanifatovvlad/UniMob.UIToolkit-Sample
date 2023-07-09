using System;
using Code.Domain;
using Code.Store;
using UniMob.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class OverviewComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;
        private readonly OverviewPage _overviewPage;

        public OverviewComponent(ViewStore viewStore, OverviewPage overviewPage)
            : base(Resources.Load<VisualTreeAsset>("Overview"))
        {
            _viewStore = viewStore;
            _overviewPage = overviewPage;
        }

        public override void Init(VisualElement root)
        {
            root.Q<ListView>("documents-list").Render(Lifetime,
                () => _overviewPage.Documents.TryGetValue(out var docs) ? docs : Array.Empty<DocumentInfo>(),
                it => new OverviewItemComponent(_viewStore, it));
        }
    }
}