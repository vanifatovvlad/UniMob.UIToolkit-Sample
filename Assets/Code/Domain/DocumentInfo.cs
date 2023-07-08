namespace Code.Domain
{
    public readonly struct DocumentInfo
    {
        public string Name { get; }

        public DocumentInfo(string name)
        {
            Name = name;
        }
    }
}