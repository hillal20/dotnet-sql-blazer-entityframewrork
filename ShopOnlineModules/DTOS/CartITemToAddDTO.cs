using System;
namespace ShopOnlineModules.DTOS
{
    public class CartITemToAddDTO
    {
        public CartITemToAddDTO()
        {
        }

        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}

