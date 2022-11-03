using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ShoppingCardAPI.Entities;
namespace ShoppingCardAPI.Repositories.Contracts
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetProductCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> ProductCategory(int id);

    }
}

