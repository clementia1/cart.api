using System;

namespace CartApi.Configuration
{
    public class RedisConfig
    {
        public string Host { get; set; } = null!;

        public TimeSpan Expiry { get; set; }
    }
}