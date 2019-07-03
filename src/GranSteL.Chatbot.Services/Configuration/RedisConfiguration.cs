namespace GranSteL.Chatbot.Services.Configuration
{
    public class RedisConfiguration : Configuration
    {
        private string _redisConnectionString;

        public string RedisConnectionString
        {
            get => _redisConnectionString;
            set => _redisConnectionString = ExpandVariable(value);
        }
    }
}
