using Ease.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Ease.Services
{
    public class CacheServices : ICacheService
    {
        private IDatabase _db;
        private readonly IDistributedCache _cache;
        public CacheServices()
        {
            _db = ConnectionHelper.RedisDbConnection.GetDatabase();
        }
        public CacheServices(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetData<T>(string key)
        {
            var data = await _cache.GetStringAsync(key);
            return data == null ? default : JsonSerializer.Deserialize<T>(data);
        }

        public async Task<bool> SetData<T>(string key, T data, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            var serializedData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(key, serializedData, options);
            return true;
        }
    }
}
