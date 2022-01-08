using EmagLite.Client.Services.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient httpClient;
        private const string UriBase = "https://localhost:44351";

        public UsersService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var response = await httpClient.GetAsync(UriBase + $"/api/Users/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            if (String.IsNullOrEmpty(responseString) || !response.IsSuccessStatusCode)
                throw new Exception("Server error. User not found");
            var user = JsonSerializer.Deserialize<User>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var response = await httpClient.GetAsync(UriBase + $"/api/Users");
            var responseString = await response.Content.ReadAsStringAsync();

            var users = JsonSerializer.Deserialize<IEnumerable<User>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return users;
        }

        public async Task RemoveUserAsync(int id)
        {
            var responseMessage = await httpClient.DeleteAsync(UriBase + $"/api/Users/{id}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }
        }
    }
}
