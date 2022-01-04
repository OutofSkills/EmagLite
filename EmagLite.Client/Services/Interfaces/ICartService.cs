using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Interfaces
{
    public interface ICartService
    {
        Task AddProductToCart(Product product);
        Task RemoveProductFromCart(int productId);
        Task<List<Product>> GetProductsAsync();
    }
}
