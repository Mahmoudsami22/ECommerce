using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrdered Product { get; set; } = default!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
