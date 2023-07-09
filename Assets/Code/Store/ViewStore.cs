using System;
using Code.Domain;
using Cysharp.Threading.Tasks;
using UniMob;

namespace Code.Store
{
    public class ViewStore : ILifetimeScope
    {
        private readonly Fetcher _fetcher;
        private Page _currentPage;

        public ViewStore(Lifetime lifetime, Fetcher fetcher)
        {
            _fetcher = fetcher;
            Lifetime = lifetime;
        }

        public Lifetime Lifetime { get; }

        [Atom] public bool IsAuthenticated => CurrentUser.HasValue;
        [Atom] public UserInfo? CurrentUser { get; private set; }

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
            CurrentPage = new OverviewPage(_fetcher);
        }

        public void ShowDocument(int documentId)
        {
            CurrentPage = new DocumentPage(_fetcher, documentId);
        }

        public async void PerformLogin(string username, string password, Action<bool> callback)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2f));
                CurrentUser = await _fetcher.Fetch<UserInfo>($"/json/{username}-{password}.json");
                callback(true);
            }
            catch
            {
                callback(false);
            }
        }
    }
}