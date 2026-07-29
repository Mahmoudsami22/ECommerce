using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Params
{
    public class ProductQueryParams
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? searchValue { get; set; }

        public ProductSortingOptions sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;

        private int pageSize = DefaultPageSize;// 5

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : (value < 1 ? DefaultPageSize : value);
        }
    }
}
