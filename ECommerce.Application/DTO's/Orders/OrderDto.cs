using ECommerce.Application.DTO_s.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Application.DTO_s.Orders
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; } = default!;

        [Required]
        public int DeliveryMethodId { get; set; }

        [Required]
        public AddressDto ShippingAddress { get; set; } = default!;
    }
}
