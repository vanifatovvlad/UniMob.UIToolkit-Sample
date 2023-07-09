using Code.Domain;
using Cysharp.Threading.Tasks;
using UniMob;

namespace Code.Store
{
    public class DocumentPage : Page
    {
        private readonly Fetcher _fetcher;
        public int DocumentId { get; }
        [Atom] public LoadStatus Status { get; private set; }
        [Atom] public Document Document { get; private set; }

        public DocumentPage(Fetcher fetcher, int documentId)
        {
            _fetcher = fetcher;
            DocumentId = documentId;

            LoadDocument().Forget();
        }

        private async UniTask LoadDocument()
        {
            Status = LoadStatus.Loading;
            Document = await _fetcher.Fetch<Document>($"/json/{DocumentId}.json");
            Status = LoadStatus.Succeed;
        }
    }
}