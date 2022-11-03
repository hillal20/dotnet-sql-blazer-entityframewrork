using System;
namespace ShopOnlineModules.DTOS
{
    public class CartItemQtyUpdateDTO
    {
        public CartItemQtyUpdateDTO()
        {
        }

        public int CartItemId { get; set; }
        public int Qty { get; set; }
    }
}

