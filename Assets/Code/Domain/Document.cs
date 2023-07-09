using Newtonsoft.Json;

namespace Code.Domain
{
    public struct Document
    {
        [JsonProperty("id")] public int ID { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
    }
}