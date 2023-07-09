using Newtonsoft.Json;

namespace Code.Domain
{
    public readonly struct UserInfo
    {
        public UserInfo(string name)
        {
            Name = name;
        }

        [field: JsonProperty("name")] public string Name { get; }
    }
}