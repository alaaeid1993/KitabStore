namespace Z_Store.Models
{
    public class CartItems
    {
        public int id { get; set; }
        public int Quantity { get; set; }
        public string Cartid { get; set; }
        public Book Book { get; set; }
    }
}
