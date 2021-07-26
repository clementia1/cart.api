using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CartApi.Configuration;
using CartApi.Entities;
using CartApi.Providers.Abstractions;
using CartApi.Services.Abstractions;
using System.Text.Json;
using StackExchange.Redis;

namespace CartApi.Providers
{
    public class CarStoreProvider : ICarStoreProvider
    {
        private readonly ILogger<CarStoreProvider> _logger;
        private readonly IRedisConnectionService _redisConnectionService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly Config _config;

        public CarStoreProvider(
            ILogger<CarStoreProvider> logger,
            IRedisConnectionService redisConnectionService,
            IJsonSerializer jsonSerializer,
            IOptions<Config> config)
        {
            _logger = logger;
            _redisConnectionService = redisConnectionService;
            _jsonSerializer = jsonSerializer;
            _config = config.Value;
        }

        public async Task<bool> Add(string key, ProductEntity entity)
        {
            var redis = GetRedisDatabase();
            var jsonString = _jsonSerializer.Serialize(entity);
            return await redis.StringSetAsync(key, jsonString, _config.Redis.Expiry);
        }

        public async Task<string> Get(string key)
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