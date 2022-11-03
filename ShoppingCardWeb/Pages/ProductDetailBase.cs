using System;
using Microsoft.AspNetCore.Components;
using ShoppingCardWeb.Services.Contracts;
using ShopOnlineModules.DTOS;
namespace ShoppingCardWeb.Pages
{
    public class ProductDetailBase: ComponentBase
    {
        public ProductDetailBase()
        {
        }

        [Parameter]
        public int ProductId { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }




        public ProductDTO Product { get; set; }

        public string ExceptionMessage { get; set; }


        // to fetch the pro
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetItem(ProductId);
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
            }
        }


        // add item to cart event  handler
        protected async Task AddToCard_click( CartItemToAddDTO cartItemToAddDTO)
        {
            try
            {
                var addingItemResponse = await  this.ShoppingCartService.PostCartItem(cartItemToAddDTO);
               
                NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
            };

        } 
    }
}

