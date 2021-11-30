using DataAccess.Repositories.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoriesRepository: Repository<Category>, ICategoryRepository
    {
        public CategoriesRepository(AppDbContext context): base(context) { }
    }
}
