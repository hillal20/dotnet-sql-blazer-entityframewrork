using ShoppingCardAPI.Entities;
using ShoppingCardAPI.Repositories.Contracts;
using ShoppingCardAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCardAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {

        // bringing the the db context which is called repsotiory 
        private  readonly Repository repository;

         public ProductRepository(Repository repository )
        {
            this.repository = repository;
        }




        // implementing the the PoductRepository interface 
        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var allProducts = await repository.products.ToListAsync();
            return allProducts;
        }


        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            var allCategories = await repository.productsCategories.ToListAsync();
            return allCategories;
        }

        public Task<ProductCategory> ProductCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}

