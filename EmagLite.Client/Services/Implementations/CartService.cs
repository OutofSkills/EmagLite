using Blazored.LocalStorage;
using EmagLite.Client.Services.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Implementations
{
    public class CartService : ICartService
    {
        private const string UriBase = "https://localhost:44351";
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public CartService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await localStorage.GetItemAsync<List<Product>>("products");
            return products;
        }

        public async Task AddProductToCart(Product product)
        {
            if (product is null)
                throw new Exception("The product was null.");

            var products = await localStorage.GetItemAsync<List<Product>>("products");
            if(products is null)
            {
                products = new List<Product>();
            }

            products.Add(product);

            await localStorage.SetItemAsync("products", products);
        }

        public async Task RemoveProductFromCart(int productId)
        {
            var products = await localStorage.GetItemAsync<List<Product>>("products");
            if (products is null)
            {
                throw new Exception("Coudn't access Local Storage.");
            }

            var product = products.Find(p => p.Id == productId);
            if(product is null)
            { 
                throw new Exception("Couldn't remove the given product."); 
            }

            products.Remove(product);
        }
    }
}
