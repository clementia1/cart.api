using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CartApi.Configuration;
using CartApi.Providers.Abstractions;
using CartApi.Services.Abstractions;
using StackExchange.Redis;

namespace CartApi.Providers
{
    public class CartStoreProvider : ICartStoreProvider
    {
        private readonly ILogger<CartStoreProvider> _logger;
        private readonly IRedisConnectionService _redisConnectionService;
        private readonly Config _config;

        public CartStoreProvider(
            ILogger<CartStoreProvider> logger,
            IRedisConnectionService redisConnectionService,
            IOptions<Config> config)
        {
            _logger = logger;
            _redisConnectionService = redisConnectionService;
            _config = config.Value;
        }

        public async Task<bool> Add(string key, string value)
        {
            var redis = GetRedisDatabase();
            return await redis.StringSetAsync(key, value, _config.Redis.Expiry);
        }

        public async Task<string?> Get(string key)
        {
            var redis = GetRedisDatabase();
            return await redis.StringGetAsync(key);
        }

        public async Task<bool> Remove(string key)
        {
            var redis = GetRedisDatabase();
            return await redis.KeyDeleteAsync(key);
        }

        public async Task<bool> Exists(string key)
        {
            var redis = GetRedisDatabase();
            return await redis.KeyExistsAsync(key);
        }
        
        private IDatabase GetRedisDatabase() => _redisConnectionService.Connection.GetDatabase();
    }
}