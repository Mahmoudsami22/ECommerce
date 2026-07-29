using ECommerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO_s.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; } = default!;

        public ICollection<BasketItemDto> Items { get; set; } = [];
    }
}
