using System;
using Microsoft.AspNetCore.Components;
using ShopOnlineModules.DTOS;
namespace ShoppingCardWeb.Pages
{
    public class DisplayProductsBase: ComponentBase
    {
        public DisplayProductsBase()
        {
        }

        // we are doig this here to define the Products variale so in the blazor page we it can recive data as a prop comming from the parent component 
        [Parameter]
        public IEnumerable<ProductDTO> Products { get; set; }

    }
}

