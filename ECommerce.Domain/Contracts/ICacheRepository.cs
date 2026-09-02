using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string cachekey, CancellationToken ct = default);
        Task SetAsync(string cachekey, string cacheValue, TimeSpan timeToLive, CancellationToken ct = default);
    }
}
