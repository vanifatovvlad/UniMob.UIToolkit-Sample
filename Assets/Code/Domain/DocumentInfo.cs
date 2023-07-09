namespace Code.Domain
{
    public readonly struct DocumentInfo
    {
        public int ID { get; }
        public string Name { get; }

        public DocumentInfo(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}