using System;

using ShoppingCardAPI.Entities;
using ShopOnlineModules.DTOS;
namespace ShoppingCardAPI.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {

        Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDTO catItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);
    }

    
}

