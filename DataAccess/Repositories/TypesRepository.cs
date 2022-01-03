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
    public class TypesRepository : Repository<ProductType>, ITypesRepository
    {
        public TypesRepository(AppDbContext _context) : base(_context)
        {
        }

        public ProductType Find(string name)
        {
            var type = _context.ProductTypes.Where(t => t.Name == name).FirstOrDefault();

            return type;
        }
    }
}
