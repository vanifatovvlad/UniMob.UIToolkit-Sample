using Code.Domain;
using UniMob;

namespace Code.Store
{
    public class DocumentPage : Page
    {
        public int DocumentId { get; }
        [Atom] public DocumentInfo Info { get; }

        public DocumentPage(int documentId)
        {
            DocumentId = documentId;
            Info = new DocumentInfo("Document " + documentId);

            // TODO fetch document
        }
    }
}