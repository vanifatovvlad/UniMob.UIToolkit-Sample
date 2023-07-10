using System.Collections.Generic;
using Code.Domain;
using Cysharp.Threading.Tasks;
using UniMob;
using UniMob.Utils;

namespace Code.Store
{
    public class OverviewPage : Page
    {
        private readonly Fetcher _fetcher;
        private readonly AsyncAtom<IList<DocumentInfo>> _documentsAtom;

        public OverviewPage(Fetcher fetcher)
        {
            _fetcher = fetcher;
            _documentsAtom = AsyncAtom.FromUniTask(Lifetime, () => LoadDocuments());
        }

        [Atom] public AsyncValue<IList<DocumentInfo>> Documents => _documentsAtom.Value;

        private async UniTask<IList<DocumentInfo>> LoadDocuments()
        {
            return await _fetcher.Fetch<DocumentInfo[]>("/json/documents.json");
        }
    }
}