﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GranSteL.Chatbot.Models.Yandex
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class MetaModel
    {
        [JsonProperty]
        public string Locale { get; set; }

        [JsonProperty]
        public string Timezone { get; set; }

        [JsonProperty]
        public string ClientId { get; set; }
    }
}
