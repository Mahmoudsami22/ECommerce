using ECommerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null, CancellationToken ct =
        default);
        Task<bool> DeleteBasketAsync(string id, CancellationToken ct = default);
    }
}
