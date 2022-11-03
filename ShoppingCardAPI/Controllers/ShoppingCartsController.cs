using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCardAPI.Repositories.Contracts;
using ShopOnlineModules.DTOS;
using ShoppingCardAPI.Extentions;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : Controller
    {

        private readonly IProductRepository productRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartsController(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.productRepository = productRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }


        // get all the cart items for one user 
        [HttpGet]
        [Route("cartItems/{userId:int}")]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetCartItems(int userId)
        {

            try
            {

                var cartItems = await shoppingCartRepository.GetItems(userId);
                var products = await productRepository.GetItems();

                if (cartItems == null)
                {
                    return NoContent();
                }

                if (products == null)
                {
                    throw new Exception("no products found");
                }

                var cartItemsDTO = DTOConversions.ConvertCartItemToDTO(products, cartItems);
                return Ok(cartItemsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // deleting item form the cart
        [HttpDelete]
        [Route("cartItem/delete/{cartItemId:int}")]
        public async Task<ActionResult<CartItemDTO?>> DeleteCartItem(int cartItemId)
        {
            try
            {
                var item = await this.shoppingCartRepository.DeleteItem(cartItemId);
                if (item == null) return NotFound();

                var product = await this.productRepository.GetItem(item.ProductId);
                if (product == null) return NotFound();

                var cartItemDto = DTOConversions.ConvertCartItemToDTO(product, item);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return  StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }


        // get one cart item
        [HttpGet]
        [Route("cartItem/{cartItemId:int}")]
        public async Task<ActionResult<CartItemDTO>> GetCartItem(int cartItemId)
        {

            var cartItem = await this.shoppingCartRepository.GetItem(cartItemId);
            var product = await this.productRepository.GetItem(cartItem.ProductId);
            try
            {
                if (cartItem == null)
                {
                    throw new Exception("no cartItems found");
                }

                if (product == null)
                {
                    throw new Exception("no products found");
                }

                var cartItemDTO = DTOConversions.ConvertCartItemToDTO(product, cartItem);
                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }





        // posting cart item 
        [HttpPost]
        [Route("addItemToCart")]
        public async Task<ActionResult<CartItemDTO?>> PostCartItem([FromBody] CartItemToAddDTO cartItemToAddDTO)
        {
            try
            {
                Console.WriteLine("============ calling post item ===========");
                var newCartItem = await this.shoppingCartRepository.AddItem(cartItemToAddDTO);
                if (newCartItem == null)
                {
                    return NoContent();
                }

                var product = await this.productRepository.GetItem(newCartItem.ProductId);
                if (product == null)
                {
                    throw new Exception($"this product is not existing : {newCartItem.ProductId}");
                }
                var newCartItemDTO = DTOConversions.ConvertCartItemToDTO(product, newCartItem);

                // it is a stander action in dotnet to return the location of the resources added to the db
                // this location details will be return authomatically in the header of the response
                // the name of the method is doing that is CreateAtAction()

                var response = CreatedAtAction(nameof(PostCartItem), new { id = newCartItemDTO?.Id }, newCartItemDTO);
                Console.WriteLine("end point response ====> " + response.Value);
                return response;
                //return Ok(newCartItemDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine("========= error happened in posting item ==============");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


       // updating the quantity of the cart item
       [HttpPatch]
       [Route("update/cartItem/{cartItemId:int}")]
       public async Task<ActionResult<CartItemDTO?>> UpdateCartItemQty(int cartItemId , [FromBody] CartItemQtyUpdateDTO cartItemQtyUpdateDTO)
        {
            try
            {
               var cartItem = await  this.shoppingCartRepository.UpdateQty(cartItemId, cartItemQtyUpdateDTO);
                if (cartItem == null)
                {
                    throw new Exception("no such cart item exist in  the db");
                }
                var product = await this.productRepository.GetItem(cartItem.ProductId);
                if(product == null)
                {
                    throw new Exception("no such product in the db");
                }

                var cartItemDTO = DTOConversions.ConvertCartItemToDTO(product, cartItem);
                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

