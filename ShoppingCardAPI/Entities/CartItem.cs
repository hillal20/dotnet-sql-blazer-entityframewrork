using System;
namespace ShoppingCardAPI.Entities
{
    public class CartItem
    {
        public CartItem()
        {
        }

        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }


    }
}

