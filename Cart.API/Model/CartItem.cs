namespace Cart.API.Model
{
    using System;

    public class CartItem
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string PictureUrl { get; set; }
    }
}