using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Commen
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}
