using EmagLite.Client.Services.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Implementations
{
    public class BrandsService : IBrandsService
    {
        private const string UriBase = "https://localhost:44351";
        private readonly HttpClient httpClient;

        public BrandsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task AddBrandAsync(ProductBrand brand)
        {
            throw new System.NotImplementedException();
        }

        public Task EditBrandAsync(ProductBrand brand)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            var responseMessage = await httpClient.GetAsync(UriBase + "/api/Brands");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }

            // convert http response data to UsersResponse object
            var brands = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductBrand>>();

            return brands;
        }

        public Task RemoveBrandAsync(int idBrand)
        {
            throw new System.NotImplementedException();
        }
    }
}
