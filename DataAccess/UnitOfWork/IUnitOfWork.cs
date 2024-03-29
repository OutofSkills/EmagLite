﻿using DataAccess.Repositories.Intefaces;
using RESTApi.DataAccess.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Repositories
        /// </summary>
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUsersRepository UsersRepository { get; }
        IBrandsRepository BrandsRepository { get; }
        ITypesRepository TypesRepository { get; }
        IOrdersRepository OrdersRepository { get; }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int SaveChanges();
    }
}
