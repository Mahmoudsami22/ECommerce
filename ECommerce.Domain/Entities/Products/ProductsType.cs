using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.Products
{
    public class ProductsType : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

    }
}
