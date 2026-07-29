using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Contracts
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cachekey, CancellationToken ct = default);
        Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive, CancellationToken ct = default);
    }
}
