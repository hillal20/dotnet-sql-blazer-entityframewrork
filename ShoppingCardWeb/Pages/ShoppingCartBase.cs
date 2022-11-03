using System;
using Microsoft.AspNetCore.Components;
using ShoppingCardWeb.Services;
using ShoppingCardWeb.Services.Contracts;
using ShopOnlineModules.DTOS;

namespace ShoppingCardWeb.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        public ShoppingCartBase()
        {
        }

        // the service which calls the urls in the backend 
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }



        // list of all the cart items 
        public List<CartItemDTO> CartItemDTOs { get; set; }


        // error message to display on the UI
        public string ErrorMessage { get; set; }


        // total price for all the items in the cart
        protected string TotalPriceOfAllItemsInCart { get; set; }



        // total quantity of all the items in the cart
        protected  int TotalQtyOfAllItemsInCart { get; set; }




        // importing all the cartitem during the creation of the component 
        protected override async Task OnInitializedAsync()
        {
            try {
                var cartItems = await this.ShoppingCartService.GetCartItems(HardCodedValues.UserId);
               this.CartItemDTOs = cartItems;
                this.MakeTheTotalCalculationOfCartItem();
            } catch (Exception ex) {

                this.ErrorMessage = ex.Message;
            }
        }






        // delete cartItem handler
        protected async Task DeleteCartItem_Click(int cartItemId)
        {

            try
            {
                var carItemDto = await this.ShoppingCartService.DeleteCartItem(cartItemId);
                // we call the remove helper to remove it from the CartItemDTOs displayed on the screen
                this.RemoveCartIem(cartItemId);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

        }





        // a helper function to  filter one cartItem from the already-fetched cartItems 
        private CartItemDTO? GetCartItem(int cartItemId)
        {
            return this.CartItemDTOs.FirstOrDefault(i => i.Id == cartItemId);
        }





        // a helper to remove the cartItem 
        private void RemoveCartIem(int cartItemId)
        {
            var cartItemDto = this.GetCartItem(cartItemId);
            CartItemDTOs.Remove(cartItemDto);
            this.MakeTheTotalCalculationOfCartItem();
        }




        // updating the quantity of the cart item
        protected async Task UpdatingCartItemQty_Click(int cartItemId , int qty)
        {

            try
            {
                if (qty > 0)
                {
                    var cartItemQtyUpdateDto = new CartItemQtyUpdateDTO
                    {
                        CartItemId = cartItemId,
                        Qty = qty
                    };

                    var updatedCartItem = await this.ShoppingCartService.UpdateCartItemQty(cartItemId, cartItemQtyUpdateDto);
                    this.UpdateItemTotalPrice(updatedCartItem);
                    this.MakeTheTotalCalculationOfCartItem();

                }
                else
                {
                    var cartItem = CartItemDTOs.FirstOrDefault(e => e.Id == cartItemId);
                    if (cartItem != null)
                    {
                        cartItem.Qty = 1;
                        cartItem.TotalPrice = cartItem.Price;
                    }

                }
               


            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }



        // calculating the totol price of all the items in the cart 
        private void SetTotalPriceOfAllItemsInCart()
        {
            this.TotalPriceOfAllItemsInCart = this.CartItemDTOs.Sum(e => e.TotalPrice).ToString("C");
        }



       // calculating the total quatity of all the items in the cart
       private void SetTotalQtyOfAllItemsInCart()
        {
            this.TotalQtyOfAllItemsInCart = this.CartItemDTOs.Sum(e => e.Qty);
        }


        // make all the calculation 
        private void MakeTheTotalCalculationOfCartItem()
        {
            this.SetTotalPriceOfAllItemsInCart();
            this.SetTotalQtyOfAllItemsInCart();
        }


        // update item total price after updating the qty
        private void UpdateItemTotalPrice(CartItemDTO cartItemDTO)
        {
            var cartItem = GetCartItem(cartItemDTO.Id);
            if(cartItem != null)
            {
                var totalPrice = cartItem.Price * cartItem.Qty;
                cartItem.TotalPrice = totalPrice;
            }
            
        }

    }
}

