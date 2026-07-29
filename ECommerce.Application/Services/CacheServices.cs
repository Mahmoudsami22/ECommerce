using ECommerce.Application.Contracts;
using ECommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.Application.Services
{
    public class CacheServices : ICacheService
    {
        private readonly ICacheRepository cacheRepository;

        public CacheServices(ICacheRepository cacheRepository)
        {
            this.cacheRepository = cacheRepository;
        }
        public Task<string?> GetAsync(string cachekey, CancellationToken ct = default)
        {
            return cacheRepository.GetAsync(cachekey, ct);

        }

        public Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions
            {

                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            return cacheRepository.SetAsync(cacheKey, json, timeToLive, ct);

        }
    }
}
