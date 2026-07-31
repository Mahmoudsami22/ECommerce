using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO_s.Orders
{
    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DeliveryTime { get; set; } = default!;
        public decimal Cost { get; set; } = default!; 
    }
}
