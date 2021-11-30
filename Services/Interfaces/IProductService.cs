using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTApi.Services.Intefaces
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        void BuyProducts();
        Task EditProductAsync(int id, Product product);
        Task RemoveProductAsync(int productId);
        IEnumerable<Product> SearchProducts(string productName);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> FindProductById(int id);
    }
}