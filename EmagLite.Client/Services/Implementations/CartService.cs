using Blazored.LocalStorage;
using EmagLite.Client.Services.Interfaces;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<Dictionary<int, Product>> GetProductsAsync()
        {
            var cartProducts = await localStorage.GetItemAsync<List<ProductInCart>>("products");
            if (cartProducts is null) return null;

            var products = new Dictionary<int, Product>();
            foreach(var cartProduct in cartProducts)
            {
                var responseMessage = await httpClient.GetAsync(UriBase + $"/api/Products/{cartProduct.ProductId}");

                if(responseMessage.IsSuccessStatusCode)
                {
                    var product = await responseMessage.Content.ReadFromJsonAsync<Product>();

                    if (product is not null)
                    {
                        products.Add(cartProduct.Quantity, product);
                    }
                }
            }

            return products;
        }

        public async Task AddProductToCart(int productId)
        {
            var products = await localStorage.GetItemAsync<List<ProductInCart>>("products");
            if(products is null)
            {
                products = new List<ProductInCart>();
            }

            var product = new ProductInCart() { ProductId = productId, Quantity = 1};
            products.Add(product);

            await localStorage.SetItemAsync("products", products);
        }

        public async Task RemoveProductFromCart(int productId)
        {
            var products = await localStorage.GetItemAsync<List<ProductInCart>>("products");
            if (products is null)
            {
                throw new Exception("Coudn't access Local Storage.");
            }

            var product = products.Find(p => p.ProductId == productId);
            if(product is null)
            { 
                throw new Exception("Couldn't remove the given product."); 
            }

            products.Remove(product);
            await localStorage.RemoveItemAsync("products");
            await localStorage.SetItemAsync("products", products);
        }

        public async Task IncreaseProductQuantity(int productId)
        {
            var products = await localStorage.GetItemAsync<List<ProductInCart>>("products");
            if (products is null)
            {
                throw new Exception("Coudn't access Local Storage.");
            }

            var product = products.Find(p => p.ProductId == productId);
            if (product is null)
            {
                throw new Exception("Couldn't find the given product.");
            }

            product.Quantity++;

            await localStorage.RemoveItemAsync("products");
            await localStorage.SetItemAsync("products", products);
        }

        public async Task DecreaseProductQuantity(int productId)
        {
            var products = await localStorage.GetItemAsync<List<ProductInCart>>("products");
            if (products is null)
            {
                throw new Exception("Coudn't access Local Storage.");
            }

            var product = products.Find(p => p.ProductId == productId);
            if (product is null)
            {
                throw new Exception("Couldn't find the given product.");
            }

            product.Quantity--;

            await localStorage.RemoveItemAsync("products");
            await localStorage.SetItemAsync("products", products);
        }

        public async Task<ProductInCart> GetProductAsync(int productId)
        {
            var products = await localStorage.GetItemAsync<List<ProductInCart>>("products");
            if (products is null)
            {
                return null;
            }

            var product = products.Find(p => p.ProductId == productId);
            return product;
        }
    }
}
