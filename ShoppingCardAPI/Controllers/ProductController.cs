using System;
using Microsoft.AspNetCore.Mvc;
using ShoppingCardAPI.Repositories.Contracts;
using ShoppingCardAPI.Extentions;
using ShopOnlineModules.DTOS;
using ShoppingCardAPI.Entities;
namespace ShoppingCardAPI.Controllers

{


    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }



        [HttpGet("/allproducts")]
        public async  Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            try
            {
                var products = await productRepository.GetItems();
                var productCategories = await productRepository.GetProductCategories();

                if(products == null || productCategories == null)
                {
                    return NotFound(" no data found ");
                }

                var productDto = DTOConversions.ConvertProductToDTO(products, productCategories);
                return Ok(productDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
           
        }


        [HttpGet("/product/{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            try
            {
                
                var product = await productRepository.GetItem(id);
                var productCatogory = await productRepository.ProductCategory(product.CategoryId);  
                if (product == null) return NotFound($"no produtct found with id : {id} ");
                var productDto = DTOConversions.ConvertProductToDTO(product, productCatogory);
                return Ok(productDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
    }
}

