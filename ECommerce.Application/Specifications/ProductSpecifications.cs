using ECommerce.Application.Params;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product,int>
    {
        public ProductSpecifications(ProductQueryParams queryParams) 
            : base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId) 
            && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId)
            && (string.IsNullOrEmpty(queryParams.searchValue) || P.Name.ToLower().Contains(queryParams.searchValue.ToLower())))
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch (queryParams.sort)
            {
                case ProductSortingOptions.NameAsc: AddOrderBy(P => P.Name); break;
                case ProductSortingOptions.NameDesc: AddOrderByDesc(P => P.Name); break;
                case ProductSortingOptions.PriceAsc: AddOrderBy(P => P.Price); break;
                case ProductSortingOptions.PriceDesc: AddOrderByDesc(P => P.Price); break;  
                _: AddOrderBy(P => P.Name); break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);

        }
        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

        }
    }
}
