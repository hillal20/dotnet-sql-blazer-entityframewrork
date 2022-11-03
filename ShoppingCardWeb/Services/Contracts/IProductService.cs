using System;
using ShopOnlineModules.DTOS;
namespace ShoppingCardWeb.Services.Contracts
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDTO>> GetItems();
        Task<ProductDTO> GetItem(int ProductId );
    }

}

