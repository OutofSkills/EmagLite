using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services.Interfaces
{
    public interface IBrandsService
    {
        void AddBrand(ProductBrand brand);
        void EditBrand(ProductBrand brand);
        ProductBrand FindBrandAsync(string name);
        Task RemoveBrandAsync(int brandId);
        Task<IEnumerable<ProductBrand>> GetBrandsAsync();
    }
}
