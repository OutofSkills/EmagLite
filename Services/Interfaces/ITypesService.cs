using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services.Interfaces
{
    public interface ITypesService
    {
        void AddType(ProductType type);
        void EditType(ProductType type);
        ProductType FindTypeAsync(string name);
        Task RemoveTypeAsync(int typeId);
        Task<IEnumerable<ProductType>> GetTypesAsync();
    }
}
