using System;

namespace Z_Store.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public Guid OrderId { get; set; }
        public int Bookid { get; set; }
        public Book Book { get; set; }
        public Order Oreder { get; set; }


    }
}
