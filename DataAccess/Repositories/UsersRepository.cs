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
    public class UsersRepository: Repository<User>, IUsersRepository
    {
        public UsersRepository(AppDbContext appDbContext): base(appDbContext) { }
    }
}
