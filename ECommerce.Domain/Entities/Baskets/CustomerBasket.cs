using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.Baskets
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;

        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
