using Newtonsoft.Json;

namespace Code.Domain
{
    public struct Document
    {
        public Document(int id, string name, string text)
        {
            ID = id;
            Name = name;
            Text = text;
        }

        [field: JsonProperty("id")] public int ID { get; }
        [field: JsonProperty("name")] public string Name { get; }
        [field: JsonProperty("text")] public string Text { get; }
    }
}