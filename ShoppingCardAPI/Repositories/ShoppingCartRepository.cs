using System;
using ShopOnlineModules.DTOS;
using ShoppingCardAPI.Entities;
using ShoppingCardAPI.Repositories.Contracts;
using ShoppingCardAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCardAPI.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        readonly private Repository repository;
        public ShoppingCartRepository(Repository repository)
        {
            this.repository = repository;
        }

        // this code is to make sure that the product is not added to the shopping cart
        public async Task<bool> IsCartItemExists(int CartId , int ProductId)
        {
            return await this.repository.cartItems.AnyAsync(c => c.ProductId == ProductId && c.CartId == CartId);
        }



        // addig item to the cart 
        public async Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDto)
        {

            if(await IsCartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (
                                from product in repository.products
                                where product.Id == cartItemToAddDto.ProductId
                                // we make sure that this product exists in the products collection in the db before we add it to the shoppingCart 
                                select new CartItem
                                {
                                    CartId = cartItemToAddDto.CartId,
                                    ProductId = product.Id,
                                    Qty = cartItemToAddDto.Qty
                                }
                              ).SingleOrDefaultAsync();


                if (item != null)
                {
                    var result = await this.repository.cartItems.AddAsync(item);
                    await this.repository.SaveChangesAsync();
                    return result.Entity;
                }
            }
            

            return null;
            
        }


        // deleting an item from the shopping cart 
        public async Task<CartItem?> DeleteItem(int id)
        {

            var item = await this.repository.cartItems.FindAsync(id);
            if (item != null)
            {
                this.repository.cartItems.Remove(item);
                await this.repository.SaveChangesAsync();
            }
            return item;
        }



        // getting the cart item stored in the ca
        public async Task<CartItem?> GetItem(int id)
        {
            return await (from cartItem in this.repository.cartItems
                          join cart in this.repository.carts
                          on cartItem.CartId equals cart.Id
                          where cartItem.Id == id
                          select new CartItem
                          {
                              CartId = cart.Id,
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty
                          }).SingleOrDefaultAsync();
        }


        // getting all cart items 
        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {

            return await (from cart in this.repository.carts
                          join cartItem in this.repository.cartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              CartId = cart.Id,
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty

                          }).ToListAsync();
        }



        // update the quantity of the cart item 
        public async Task<CartItem?> UpdateQty(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDto)
        {

            var updatedItem = await this.repository.cartItems.FindAsync(id);
            if(updatedItem != null)
            {
                updatedItem.Qty = cartItemQtyUpdateDto.Qty;
                this.repository.SaveChanges();
                return updatedItem;
            }

            return null;
        }
    }
}

