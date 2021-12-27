using DataAccess.UnitOfWork;
using Models;
using RESTApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await unitOfWork.UsersRepository.GetById(id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await unitOfWork.UsersRepository.GetAll();

            return users;
        }

        public async Task RemoveUserAsync(int id)
        {
            await unitOfWork.UsersRepository.Delete(id);
            unitOfWork.SaveChanges();
        }
    }
}
