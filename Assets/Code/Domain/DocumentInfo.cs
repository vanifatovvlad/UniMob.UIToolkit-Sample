using Newtonsoft.Json;

namespace Code.Domain
{
    public readonly struct DocumentInfo
    {
        public DocumentInfo(int id, string name)
        {
            ID = id;
            Name = name;
        }

        [field: JsonProperty("id")] public int ID { get; }
        [field: JsonProperty("name")] public string Name { get; }
    }
}