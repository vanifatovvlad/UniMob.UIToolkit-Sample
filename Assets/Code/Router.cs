using Code.Store;

namespace Code
{
    public class Router
    {
        private readonly ViewStore _viewStore;

        public Router(ViewStore viewStore)
        {
            _viewStore = viewStore;
        }

        public void Go(string url)
        {
            if (url == "/document/")
            {
                _viewStore.ShowOverview();
            }
            else if (url.StartsWith("/document/") &&
                     int.TryParse(url.Substring("/document/".Length), out var documentId))
            {
                _viewStore.ShowDocument(documentId);
            }
            else
            {
                _viewStore.ShowOverview();
            }
        }
    }
}