using System;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ShopOnlineModules.DTOS;
using ShoppingCardWeb.Services.Contracts;
namespace ShoppingCardWeb.Services
{
    public class ShoppingCartService : IShoppingCartService 
    {
        private readonly HttpClient httpClient;
        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        // call the api to get the cart item 
        async Task<CartItemDTO?> IShoppingCartService.GetCartItem(int cartItemId)
        {
            try
            {
                var cartItemDto = await this.httpClient.GetFromJsonAsync<CartItemDTO>($"api/ShoppingCarts/cartItem/{cartItemId}");
                return cartItemDto;

            }
            catch(Exception)
            {
                throw  new Exception("error in fetching cart item ");
            };
        }


        // call the api to get the cart itmes 
        async Task<List<CartItemDTO>?> IShoppingCartService.GetCartItems(int userId)
        {

            try
            {
                var cartItemsDto =  await  this.httpClient.GetFromJsonAsync<IEnumerable<CartItemDTO>>($"api/ShoppingCarts/cartItems/{userId}");
                return cartItemsDto?.ToList();

            }
            catch (Exception)
            {
                throw new Exception("error in fetching cart item ");
            };
        }




        // call the api to post the cart item 
       async Task<CartItemDTO?> IShoppingCartService.PostCartItem(CartItemToAddDTO cartItemToAddDTO)
        {
            try
            {
                var postedItemResponse =  await this.httpClient.PostAsJsonAsync<CartItemToAddDTO>("api/ShoppingCarts/addItemToCart", cartItemToAddDTO);


                if (postedItemResponse.IsSuccessStatusCode)
                {
                   
                    if (postedItemResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                       
                        return default(CartItemDTO);
                    }
                    return await postedItemResponse.Content.ReadFromJsonAsync<CartItemDTO>();
                }
                else
                {
                    var exceptionMessage = await postedItemResponse.Content.ReadAsStringAsync();
                    throw new Exception($" ===> content conatian the error : {exceptionMessage}");
                }

            }
            catch(Exception ex)
            {
                throw new Exception($"error in  posting the item : {ex.Message}");
            }
        }



        // deleting cart item 
       async Task<CartItemDTO?> IShoppingCartService.DeleteCartItem(int cartItemId)
        {
            try
            {
                var response = await this.httpClient.DeleteAsync($"api/ShoppingCarts/cartItem/delete/{cartItemId}");

                // if the reponse falls in the success respose range we read the response 
                if (response.IsSuccessStatusCode)
                {
                   return await  response.Content.ReadFromJsonAsync<CartItemDTO>();
                }
                // if it is not successfull we return null 
                return default(CartItemDTO);
            }
            catch (Exception ex)
            {
                throw new Exception($"error in  deleting  the item : {ex.Message}");
            }
            
        }


        // updating the cart item qty  
      async Task<CartItemDTO?> IShoppingCartService.UpdateCartItemQty(int cartItemId, CartItemQtyUpdateDTO cartItemQtyUpdateDTO)
        {
            try
            {
                // creating json format 
                var jsonObj = JsonConvert.SerializeObject(cartItemQtyUpdateDTO);
                // creating the content for the patch 
                var patchContent = new StringContent(jsonObj, System.Text.Encoding.UTF8, "application/json-patch+json");

                var response = await this.httpClient.PatchAsync($"api/ShoppingCarts/update/cartItem/{cartItemId}", patchContent);

                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
           
        }
    }
}

