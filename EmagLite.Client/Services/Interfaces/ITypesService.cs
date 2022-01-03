using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Interfaces
{
    public interface ITypesService
    {
        Task<IEnumerable<ProductType>> GetTypesAsync();
        Task AddTypeAsync(ProductType type);
        Task RemoveTypeAsync(int idType);
        Task EditTypeAsync(ProductType type);
    }
}
