using System;
using Microsoft.AspNetCore.Mvc;
using ShoppingCardAPI.Repositories.Contracts;
using ShoppingCardAPI.Entities;
using ShopOnlineModules.DTOS;
namespace ShoppingCardAPI.Controllers

{


    [Route("api/[Controller")]
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
            var products =  await productRepository.GetItems();
            return Ok(products);
        }
    }
}

