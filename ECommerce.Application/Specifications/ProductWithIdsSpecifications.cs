using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Specifications
{
    public class ProductWithIdsSpecifications : BaseSpecifications<Product,int>
    {
        public ProductWithIdsSpecifications(IEnumerable<int> ids):base(P => ids.Contains(P.Id))
        {
            
        }
    }
}
