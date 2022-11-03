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




        ////////////////////////////////////////////////  implementing the the PoductRepository interface
        ///////////////////////////////////////////////////////////////////////////////////////////////////////



        public async Task<Product> GetItem(int id)
        {
            var product = await repository.products.FindAsync(id);
            return product!;
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



        public async Task<ProductCategory> ProductCategory(int id)
        {
            var productCategory = await repository.productsCategories.SingleOrDefaultAsync(c => c.Id == id);
            return productCategory!;
        }
    }
}

