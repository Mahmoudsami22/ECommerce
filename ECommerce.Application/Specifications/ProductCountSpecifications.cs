using ECommerce.Application.Params;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Specifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product,int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams)
            : base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId)
            && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId)
            && (string.IsNullOrEmpty(queryParams.searchValue) || P.Name.ToLower().Contains(queryParams.searchValue.ToLower())))
        {
            
        }
    }
}
