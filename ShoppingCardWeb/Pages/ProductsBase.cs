using System;
using Microsoft.AspNetCore.Components;
using ShopOnlineModules.DTOS;
using ShoppingCardWeb.Services.Contracts;

namespace ShoppingCardWeb.Pages
{
    public class ProductsBase : ComponentBase
    {
        public ProductsBase()
        {
        }

        // we need to inject the productservice object which generated from ProductService Class
        // but we need to regester it in the CDN in the Program.cs 
        [Inject]
        public IProductService ProductService { get; set; }


        // we need this variable becuase it is the one gives the data in the razor page 
        public IEnumerable<ProductDTO> Products { get; set; }



        // this method is associated with blazor lifecycle 
        protected override async Task OnInitializedAsync()
        {
            // we write code here to get the data form the api 
            this.Products = await ProductService.GetItems();
        }
    }
}

