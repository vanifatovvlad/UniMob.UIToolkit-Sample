using System;
using Code.Domain;
using Cysharp.Threading.Tasks;
using UniMob;
using UniMob.Utils;

namespace Code.Store
{
    public class DocumentPage : Page
    {
        private readonly Fetcher _fetcher;
        private readonly ViewStore _viewStore;
        private readonly AsyncAtom<Document> _documentAtom;

        public DocumentPage(Fetcher fetcher, ViewStore viewStore, int documentId)
        {
            _fetcher = fetcher;
            _viewStore = viewStore;
            DocumentId = documentId;
            _documentAtom = AsyncAtom.FromUniTask<Document>(Lifetime, sink => sink(LoadDocument()));

            LoadDocument().Forget();
        }

        public int DocumentId { get; }

        [Atom] public AsyncValue<Document> Document => _documentAtom.Value;

        private async UniTask<Document> LoadDocument()
        {
            if (!_viewStore.IsAuthenticated)
            {
                throw new InvalidOperationException("Not authenticated");
            }

            return await _fetcher.Fetch<Document>($"/json/{DocumentId}.json");
        }
    }
}