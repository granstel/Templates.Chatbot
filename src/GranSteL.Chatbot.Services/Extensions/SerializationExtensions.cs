using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GranSteL.Chatbot.Services.Extensions
{
    public static class SerializationExtensions
    {
        private static readonly ILogger Log;

        static SerializationExtensions()
        {
            Log = InternalLoggerFactory.CreateLogger(nameof(SerializationExtensions));
        }
        
        public static string Serialize(this object obj, JsonSerializerSettings settings = null)
        {
            if (!(obj is string result))
            {
                result = JsonConvert.SerializeObject(obj, null, settings);
            }

            return result;
        }

        public static T Deserialize<T>(this object obj, JsonSerializerSettings settings = null)
        {
            try
            {
                if (obj is string serialized)
                {
                    return JsonConvert.DeserializeObject<T>(serialized, settings);
                }
            }
            catch (Exception e)
            {
                Log?.LogError(e, "Error while deserialize");
            }

            return default;
        }
    }
}
