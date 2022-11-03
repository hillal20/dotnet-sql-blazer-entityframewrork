using System;
using ShopOnlineModules.DTOS;

namespace ShoppingCardWeb.Services.Contracts
{
    public interface IShoppingCartService
    {

        public Task<List<CartItemDTO>> GetCartItems(int userId);
        public Task<CartItemDTO> GetCartItem(int cartItemId);
        public Task<CartItemDTO?> PostCartItem(CartItemToAddDTO cartItemToAddDTO);
        public Task<CartItemDTO> DeleteCartItem(int cartItemId);
        public Task<CartItemDTO> UpdateCartItemQty(int cartItemId, CartItemQtyUpdateDTO cartItemQtyUpdateDTO);

    }
}

