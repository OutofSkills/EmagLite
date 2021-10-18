using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Intefaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> SearchByName(string productName);
    }
}
