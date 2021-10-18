﻿using DataAccess.Repositories.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context): base(context) { }


        public IEnumerable<Product> SearchByName(string productName)
        {
            return _context.Products.Where(p => p.Name == productName).ToList();
        }
    }
}
