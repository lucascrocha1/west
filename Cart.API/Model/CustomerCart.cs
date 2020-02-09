namespace Cart.API.Model
{
    using System.Collections.Generic;

    public class CustomerCart
    {
        public string CustomerId { get; set; }

        public List<CartItem> Items { get; set; }
    }
}