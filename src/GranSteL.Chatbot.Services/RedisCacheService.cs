using System;
using GranSteL.Chatbot.Services.Configuration;
using GranSteL.Chatbot.Services.Extensions;
using Newtonsoft.Json;
using NLog;
using StackExchange.Redis;

namespace GranSteL.Chatbot.Services
{
    public class RedisCacheService : ICacheService, IDisposable
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _dataBase;
        private readonly JsonSerializerSettings _serializerSettings;

        private readonly Logger _log = LogManager.GetLogger(nameof(RedisCacheService));

        public RedisCacheService(RedisConfiguration configuration)
        {
            _redis = ConnectionMultiplexer.Connect(configuration.RedisConnectionString);
            _dataBase = _redis.GetDatabase();

            _serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public async void AddAsync(string key, object data, TimeSpan? timeOut = null)
        {
            if (data == null)
                return;

            try
            {
                if (!(data is string value))
                {
                    value = data.Serialize(_serializerSettings);
                }

                await _dataBase.StringSetAsync(key, value, timeOut);
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        public bool TryGet<T>(string key, out T data)
        {
            try
            {
                var value = _dataBase.StringGet(key).ToString();

                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception($"Value for {key} is null");
                }

                data = value.Deserialize<T>(_serializerSettings);
            }
            catch (Exception e)
            {
                _log.Error(e);

                data = default(T);

                return false;
            }

            return true;
        }

        public bool Exist(string key)
        {
            try
            {
                return _dataBase.KeyExists(key);
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            return false;
        }

        public async void DeleteAsync(string key)
        {
            try
            {
                await _dataBase.KeyDeleteAsync(key);
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
    }
}
