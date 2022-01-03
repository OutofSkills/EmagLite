using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Interfaces
{
    public interface IBrandsService
    {
        Task<IEnumerable<ProductBrand>> GetBrandsAsync();
        Task AddBrandAsync(ProductBrand brand);
        Task RemoveBrandAsync(int idBrand);
        Task EditBrandAsync(ProductBrand brand);
    }
}
