using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services.Interfaces
{
    public interface IOrdersService
    {
        void MakeOrder(Order order);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<OrderProduct>> GetOrderProductsAsync(int orderId);
        Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
        Task RemoveOrder(int orderId);
    }
}
