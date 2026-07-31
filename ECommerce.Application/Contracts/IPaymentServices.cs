using ECommerce.Application.Common;
using ECommerce.Application.DTO_s.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Contracts
{
    public interface IPaymentServices
    {
        Task<Result<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken ct = default);
    }
}
