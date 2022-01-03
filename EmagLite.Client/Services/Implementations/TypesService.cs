using EmagLite.Client.Services.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Implementations
{
    public class TypesService : ITypesService
    {
        private const string UriBase = "https://localhost:44351";
        private readonly HttpClient httpClient;

        public TypesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task AddTypeAsync(ProductType type)
        {
            throw new System.NotImplementedException();
        }

        public Task EditTypeAsync(ProductType type)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ProductType>> GetTypesAsync()
        {
            var responseMessage = await httpClient.GetAsync(UriBase + "/api/Types");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }

            // convert http response data to UsersResponse object
            var types = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductType>>();

            return types;
        }

        public Task RemoveTypeAsync(int idType)
        {
            throw new System.NotImplementedException();
        }
    }
}
