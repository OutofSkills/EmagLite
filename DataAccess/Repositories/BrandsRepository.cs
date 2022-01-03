using DataAccess;
using DataAccess.Repositories;
using Models;
using RESTApi.DataAccess.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.DataAccess.Repositories
{
    public class BrandsRepository : Repository<ProductBrand>, IBrandsRepository
    {
        public BrandsRepository(AppDbContext _context) : base(_context)
        {
        }

        public ProductBrand Find(string name)
        {
            var brand = _context.ProductBrands.Where(b => b.Name == name).FirstOrDefault();

            return brand;
        }
    }
}
