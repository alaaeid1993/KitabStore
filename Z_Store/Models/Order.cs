using System;
using System.Collections.Generic;

namespace Z_Store.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new();
        public DateTime Dateorder { get; set; }
        public int OrderTotal { get; set; }
    }
}
