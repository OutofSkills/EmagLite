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
    public class OrdersService : IOrdersService
    {
        private const string UriBase = "https://localhost:44351";
        private readonly HttpClient httpClient;

        public OrdersService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var responseMessage = await httpClient.GetAsync(UriBase + "/api/Orders");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }

            // convert http response data to UsersResponse object
            var orders = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Order>>();

            return orders;
        }

        public async Task<IEnumerable<OrderProduct>> GetOrderProductsAsync(int orderId)
        {
            var responseMessage = await httpClient.GetAsync(UriBase + $"/api/OrderProducts/{orderId}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }

            // convert http response data to OrderProduct object
            var orderProducts = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<OrderProduct>>();

            foreach(var orderProduct in orderProducts)
            {
                var response = await httpClient.GetAsync(UriBase + $"/api/Products/{orderProduct.ProductId}");

                if (!responseMessage.IsSuccessStatusCode)
                {
                    // set error message for display, log to console and return
                    var errorMessage = responseMessage.ReasonPhrase;
                    throw new Exception($"There was an error! {errorMessage}");
                }

                // convert http response data to Product object
                orderProduct.Product = await response.Content.ReadFromJsonAsync<Product>();
            }

            return orderProducts;
        }

        public async Task MakeOrderAsync(Order order)
        {
            // Serialize the object to JSON format to send it in body request
            var requestContent = SerializeObject(order);
            var responseMessage = await httpClient.PostAsync(UriBase + "/api/Orders", requestContent);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }
        }

        private StringContent SerializeObject(Order order)
        {
            var serializedProduct = JsonSerializer.Serialize(order);
            var requestContent = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

            return requestContent;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(int userId)
        {
            var responseMessage = await httpClient.GetAsync(UriBase + $"/api/Orders/{userId}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                var errorMessage = responseMessage.ReasonPhrase;
                throw new Exception($"There was an error! {errorMessage}");
            }

            // convert http response data to OrderProduct object
            var orders = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Order>>();

            return orders;
        }
    }
}
