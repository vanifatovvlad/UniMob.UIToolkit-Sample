using System;
using System.Collections.Generic;
using System.Linq;
using Code.Domain;
using Cysharp.Threading.Tasks;
using UniMob;

namespace Code.Store
{
    public class OverviewPage : Page
    {
        private readonly Fetcher _fetcher;

        public OverviewPage(Fetcher fetcher)
        {
            _fetcher = fetcher;

            LoadDocuments().Forget();
        }

        [Atom] public LoadStatus Status { get; private set; }
        [Atom] public IList<DocumentInfo> Documents { get; private set; } = Array.Empty<DocumentInfo>();

        private async UniTask LoadDocuments()
        {
            Status = LoadStatus.Loading;
            Documents = await _fetcher.Fetch<DocumentInfo[]>("/json/documents.json");
            Status = LoadStatus.Succeed;
        }
    }
}