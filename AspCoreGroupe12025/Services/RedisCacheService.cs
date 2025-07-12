using StackExchange.Redis;
using System.Text.Json;

namespace AspCoreGroupe12025.Services
{
    public class RedisCacheService
    {
        private readonly IDatabase _cacheDb;

        public RedisCacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDb = redis.GetDatabase();
        }

        public async Task SetDataAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _cacheDb.StringSetAsync(key, jsonData, expiry);
        }

        public async Task<T?> GetDataAsync<T>(string key)
        {
            var value = await _cacheDb.StringGetAsync(key);
            if (!value.IsNullOrEmpty)
                return JsonSerializer.Deserialize<T>(value);
            return default;
        }

        public async Task<bool> RemoveDataAsync(string key)
        {
            return await _cacheDb.KeyDeleteAsync(key);
        }
    }
}
