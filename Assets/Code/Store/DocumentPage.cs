namespace Code.Store
{
    public class DocumentPage : Page
    {
        public int DocumentId { get; }
        public string DocumentName { get; }

        public DocumentPage(int documentId)
        {
            DocumentId = documentId;
            DocumentName = "Document " + documentId;

            // TODO fetch document
        }
    }
}