using ECommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
        {
            var value = await _database.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string cachekey, string cacheValue, TimeSpan timeToLive, CancellationToken ct = default)
        {
             await _database.StringSetAsync(cachekey, cacheValue, timeToLive);
        }
    }
}
