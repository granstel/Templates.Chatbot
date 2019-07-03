using System;

namespace GranSteL.Chatbot.Services
{
    public interface ICacheService
    {
        void AddAsync(string key, object data, TimeSpan? timeOut = null);

        bool TryGet<T>(string key, out T data);

        bool Exist(string key);

        void DeleteAsync(string key);
    }
}