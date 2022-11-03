using System;
// we create this projects to cover the DTO ( data trasfer object ) classes which cary out the data between the front end and the backend 
// this class contain the product data which comes from mutiple entity classes ( DB classes )
namespace ShopOnlineModules.DTOS
{
    public class CartItemDTO
    {
        public CartItemDTO()
        {
        }


        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public string ProctuctDescription { get; set; }
        public string ProductImageURL { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Qty { get; set; }
    }
}

