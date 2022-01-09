using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Interfaces
{
    public interface IOrdersService
    {
        Task MakeOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<OrderProduct>> GetOrderProductsAsync(int orderId);
        Task<IEnumerable<Order>> GetUserOrders(int userId);
    }
}
