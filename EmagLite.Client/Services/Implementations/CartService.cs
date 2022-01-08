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

        public async Task<List<CartItem>> GetProductsAsync()
        {
            var products = new List<CartItem>();

            var storageProducts = await localStorage.GetItemAsync<List<ProductInStorage>>("products");
            if (storageProducts is null) return products;

            foreach(var storageProduct in storageProducts)
            {
                var responseMessage = await httpClient.GetAsync(UriBase + $"/api/Products/{storageProduct.ProductId}");

                if(responseMessage.IsSuccessStatusCode)
                {
                    var product = await responseMessage.Content.ReadFromJsonAsync<Product>();

                    if (product is not null)
                    {
                        products.Add(new() { Product = product, Quantity = storageProduct.Quantity});
                    }
                }
            }

            return products;
        }

        public async Task AddProductToCart(int productId)
        {
            var products = await localStorage.GetItemAsync<List<ProductInStorage>>("products");
            if(products is null)
            {
                products = new List<ProductInStorage>();
            }

            var product = new ProductInStorage() { ProductId = productId, Quantity = 1};
            products.Add(product);

            await localStorage.SetItemAsync("products", products);
        }

        public async Task RemoveProductFromCart(int productId)
        {
            var products = await localStorage.GetItemAsync<List<ProductInStorage>>("products");
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
            var products = await localStorage.GetItemAsync<List<ProductInStorage>>("products");
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
            var products = await localStorage.GetItemAsync<List<ProductInStorage>>("products");
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

        public async Task<CartItem> GetProductAsync(int productId)
        {
            Product product = null;

            var products = await localStorage.GetItemAsync<List<ProductInStorage>>("products");
            if (products is null)
            {
                return null;
            }

            var storageProduct = products.Find(p => p.ProductId == productId);

            if (storageProduct is null)
            {
                return null;
            }

            var responseMessage = await httpClient.GetAsync(UriBase + $"/api/Products/{storageProduct.ProductId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                product = await responseMessage.Content.ReadFromJsonAsync<Product>();
            }

            return new CartItem() { Product=product, Quantity = storageProduct.Quantity };
        }
    }
}
