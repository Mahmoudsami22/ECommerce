using ECommerce.Domain.Commen;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.Products
{
    public class ProductsBrand : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
