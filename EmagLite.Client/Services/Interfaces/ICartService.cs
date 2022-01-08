using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Interfaces
{
    public interface ICartService
    {
        Task AddProductToCart(int productId);
        Task IncreaseProductQuantity(int productId);
        Task DecreaseProductQuantity(int productId);
        Task RemoveProductFromCart(int productId);
        Task<List<CartItem>> GetProductsAsync();
        Task<CartItem> GetProductAsync(int productId);
    }
}
