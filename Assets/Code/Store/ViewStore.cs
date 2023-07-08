using Code.Domain;
using UniMob;

namespace Code.Store
{
    public class ViewStore : Store
    {
        private Page _currentPage;

        //TODO fetch
        public ViewStore(Lifetime lifetime) : base(lifetime)
        {
        }

        [Atom] public bool IsAuthenticated => CurrentUser.Name != null;
        [Atom] public UserInfo CurrentUser => new UserInfo("test user");

        [Atom] public string CurrentPath => CurrentPage switch
        {
            OverviewPage overviewPage => "/document/",
            DocumentPage documentPage => $"/document/{documentPage.DocumentId}",
            _ => "/document/",
        };

        [Atom] public Page CurrentPage
        {
            get => _currentPage;
            private set
            {
                _currentPage?.Dispose();
                _currentPage = value;
            }
        }

        public void ShowOverview()
        {
            CurrentPage = new OverviewPage();
        }

        public void ShowDocument(int documentId)
        {
            CurrentPage = new DocumentPage(documentId);
        }

        //TODO add login
    }
}