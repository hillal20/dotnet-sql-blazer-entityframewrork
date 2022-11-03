using System;
using System.Net.Http.Json;
using ShopOnlineModules.DTOS;
using ShoppingCardWeb.Services.Contracts;

namespace ShoppingCardWeb.Services
{
    public class ProductService : IProductService
    {

        readonly private HttpClient httpClient;
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        //  http call to get products 
        public async Task<IEnumerable<ProductDTO>> GetItems()
        {
            try
            {
                var products = await this.httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>("/allProducts");
                return products!;
            }
            catch (Exception)
            {
                throw new Exception($"faillure in making http call to fetch products");
            }
        }


        //  http call to get one product by id 
        public async Task<ProductDTO> GetItem(int ProductId )
        {
            try
            {
                var respose  = await this.httpClient.GetAsync($"/product/{ProductId}");
                if (respose.IsSuccessStatusCode)
                {
                    if(respose.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        // it is equal to en empty object of type ProductDTO 
                        return default(ProductDTO)!;
                    }
                    return (await respose.Content.ReadFromJsonAsync<ProductDTO>())!;
                }
                else
                {
                    var message = await respose.Content.ReadAsStringAsync();
                    throw new Exception(message);
                };
            }
            catch (Exception)
            {
                throw new Exception($"faillure in making http call to fetch the product with id : {ProductId}");
            }
        }
    }
}


