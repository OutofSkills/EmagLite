using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;
using EmagLite.Client.Authentication.Interfaces;
using Models.ViewModels;
using EmagLite.Client.Authentication;

namespace EmagLite.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.authStateProvider = authStateProvider;
            this.localStorage = localStorage;
        }

        public async Task<LoginResponse> Login(LoginRequest userForAtuthetication)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", userForAtuthetication.Email),
                new KeyValuePair<string, string>("password", userForAtuthetication.Password)
            });

            var authResult = await httpClient.PostAsync("api/tokens/login", data);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<LoginResponse>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await localStorage.SetItemAsync("authToken", result.Token);

            ((AuthStateProvider)authStateProvider).NotifyUserAuthentication(result.Token);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return result;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)authStateProvider).NotifyUserLogout();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> Register(RegisterRequest model)
        {
            var registerResult = await httpClient.PostAsJsonAsync("/api/Tokens/register", model);

            if (registerResult.IsSuccessStatusCode is false)
            {
                return "Something went wrong";
            }

            return "Success";
        }

        public async Task<bool> ChangePasswordAsync(ResetPasswordModel resetPasswordModel, int id)
        {
            var result = await httpClient.PutAsJsonAsync($"/api/Users/{id}", resetPasswordModel);

            return result.IsSuccessStatusCode;
        }
    }
}
