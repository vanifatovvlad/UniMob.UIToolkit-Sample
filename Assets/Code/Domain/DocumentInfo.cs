﻿using Newtonsoft.Json;

namespace Code.Domain
{
    public struct DocumentInfo
    {
        [JsonProperty("id")] public int ID { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}