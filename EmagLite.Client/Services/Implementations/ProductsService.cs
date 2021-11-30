using EmagLite.Client.Services.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Implementations
{
    public class ProductsService : IProductsService
    {
        private const string UriBase = "https://localhost:44351";
        private readonly HttpClient httpClient;

        public ProductsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task AddProductAsync(Product product)
        {
            // Serialize the object to JSON format to send it in body request
            var requestContent = SerializeObject(product);
            var responseMessage = await httpClient.PostAsync(UriBase + "/api/Products", requestContent);

            if(!responseMessage.IsSuccessStatusCode)
            {
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }
        }

        public Task BuyProductAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var responseMessage = await httpClient.GetAsync(UriBase + "/api/Products");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }

            // convert http response data to UsersResponse object
            var products = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Product>>();

            return products;
        }

        public Task RemoveProductAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        private StringContent SerializeObject(Product product)
        {
            var serializedProduct = JsonSerializer.Serialize(product);
            var requestContent = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

            return requestContent;
        }
    }
}
