using System;
using System.Collections.Generic;
using Code.Domain;
using Cysharp.Threading.Tasks;
using UniMob;

namespace Code.Store
{
    public class OverviewPage : Page
    {
        public OverviewPage()
        {
            LoadDocuments().Forget();
        }

        [Atom] public LoadStatus Status { get; private set; } = LoadStatus.Loading;
        [Atom] public IList<DocumentInfo> Documents { get; private set; } = Array.Empty<DocumentInfo>();

        private async UniTask LoadDocuments()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            // TODO load documents
            Documents = new[]
            {
                new DocumentInfo("Document 0"),
                new DocumentInfo("Document 1"),
                new DocumentInfo("Document 2"),
                new DocumentInfo("Document 3"),
            };
            Status = LoadStatus.Succeed;
        }
    }
}