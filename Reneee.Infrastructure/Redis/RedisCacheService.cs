using Reneee.Application.Contracts.ThirdService;
using StackExchange.Redis;
using System.Text.Json;

namespace Reneee.Infrastructure.Redis
{
    public class RedisCacheService(IConnectionMultiplexer connectionMultiplexer, RedisSettings redisSettings) : ICacheService
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        private readonly RedisSettings _redisSettings = redisSettings;

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(_redisSettings.InstanceName + key);
            if (value.IsNullOrEmpty)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(_redisSettings.InstanceName + key, serializedValue, expiration);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(_redisSettings.InstanceName + key);
        }

        public async Task<bool> AcquireLockAsync(string key, TimeSpan expiration)
        {
            var isLocked = await _database.StringSetAsync(key, "locked", expiration, When.NotExists);
            return isLocked;
        }

        public async Task ReleaseLockAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }


}
