using System;
using StackExchange.Redis;

namespace CartApi.Services.Abstractions
{
    public interface IRedisConnectionService
    {
        public IConnectionMultiplexer Connection { get; }
    }
}