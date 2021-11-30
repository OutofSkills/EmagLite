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
    public class CategoriesService : ICategoriesService
    {
        private const string UriBase = "https://localhost:44351";
        private readonly HttpClient httpClient;

        public CategoriesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task AddCategoryAsync(Category category)
        {
            // Serialize the object to JSON format to send it in body request
            var requestContent = SerializeObject(category);
            var responseMessage = await httpClient.PostAsync(UriBase + "/api/Categories", requestContent);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }
        }

        public Task DeleteCategoryAsync(int idCategory)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var responseMessage = await httpClient.GetAsync(UriBase + "/api/Categories");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }

            // convert http response data to UsersResponse object
            var categories = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Category>>();

            return categories;
        }
        private StringContent SerializeObject(Category category)
        {
            var serializedProduct = JsonSerializer.Serialize(category);
            var requestContent = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

            return requestContent;
        }
    }
}
