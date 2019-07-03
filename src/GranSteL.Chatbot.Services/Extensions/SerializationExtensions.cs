using System;
using Newtonsoft.Json;
using NLog;

namespace GranSteL.Chatbot.Services.Extensions
{
    public static class SerializationExtensions
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        
        public static string Serialize(this object obj, JsonSerializerSettings settings = null)
        {
            if (!(obj is string result))
            {
                result = JsonConvert.SerializeObject(obj, settings);
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
                Log.Warn(e);
            }

            return default(T);
        }
    }
}
