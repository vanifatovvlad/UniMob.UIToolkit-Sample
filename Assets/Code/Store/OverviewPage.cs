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
            Documents = Enumerable.Range(0, 100).Select(i => new DocumentInfo(i, $"Document {i}")).ToList();
            Status = LoadStatus.Succeed;
        }
    }
}