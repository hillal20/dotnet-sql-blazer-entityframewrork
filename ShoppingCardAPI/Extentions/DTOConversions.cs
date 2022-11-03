using System;
using ShopOnlineModules.DTOS;
using ShoppingCardAPI.Entities;
namespace ShoppingCardAPI.Extentions

{
    public static class DTOConversions
    {


        // ConvertProductToDTO is doing method overloading 
        public static IEnumerable<ProductDTO> ConvertProductToDTO(this IEnumerable<Product> products , IEnumerable<ProductCategory> productCategories)
        {

            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Qty = product.Qty,
                        CategoryId = product.CategoryId,
                        CategoryName = productCategory.Name,
                        ImageURL = product.ImageURL

                    }
                    ).ToList();
        }


        public static ProductDTO  ConvertProductToDTO(this Product product, ProductCategory productCategory)
        {

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name,
                ImageURL = product.ImageURL

            };
                 
        }


        // method oveloading 
        public static IEnumerable<CartItemDTO> ConvertCartItemToDTO(this IEnumerable<Product> products, IEnumerable<CartItem> cartItems)
        {

            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDTO
                    {
                        Id = cartItem.Id,
                        CartId = cartItem.CartId,
                        ProductName = product.Name,
                        ProctuctDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        TotalPrice = cartItem.Qty * product.Price,
                        Price = product.Price,
                        Qty = product.Qty,
                        ProductId = product.Id
                    }).ToList();

        }

        public static CartItemDTO? ConvertCartItemToDTO(this Product product, CartItem cartItem)
        {

            return new CartItemDTO
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                ProductName = product.Name,
                ProctuctDescription = product.Description,
                ProductImageURL = product.ImageURL,
                TotalPrice = cartItem.Qty * product.Price,
                Price = product.Price,
                Qty = product.Qty,
                ProductId = product.Id
            };

        }
    }
}

