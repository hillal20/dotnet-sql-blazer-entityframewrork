using System;
namespace ShopOnlineModules.DTOS
{
    public class CartItemToAddDTO
    {
        public CartItemToAddDTO()
        {
        }

        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}

