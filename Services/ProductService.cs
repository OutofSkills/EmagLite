using AutoMapper;
using DataAccess.UnitOfWork;
using Models;
using RESTApi.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddProductAsync(Product product)
        {
            if (product is null)
                throw new Exception("The product object was null.");

            var productCategory = await unitOfWork.CategoryRepository.GetById(product.CategoryId);
            if (productCategory is null)
                throw new Exception("The selected product category doesn't exist.");

            product.Category = productCategory;

            unitOfWork.ProductRepository.Insert(product);
            unitOfWork.SaveChanges();
        }

        public IEnumerable<Product> SearchProducts(string productName)
        {
            var products = unitOfWork.ProductRepository.SearchByName(productName);

            return products;
        }

        public async Task RemoveProductAsync(int productId)
        {
            try
            {
                await unitOfWork.ProductRepository.Delete(productId);
                unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task EditProductAsync(int id, Product product)
        {
            if (product is null)
                throw new Exception("The product object was null.");

            var productCategory = await unitOfWork.CategoryRepository.GetById(product.CategoryId);
            if (productCategory is null)
                throw new Exception("The selected product category doesn't exist.");

            product.Category = productCategory;

            unitOfWork.ProductRepository.Update(product);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await unitOfWork.ProductRepository.GetAll();

            foreach(var product in products)
            {
                product.Brand = await unitOfWork.BrandsRepository.GetById(product.BrandId);
                product.Category = await unitOfWork.CategoryRepository.GetById(product.CategoryId);
                product.Type = await unitOfWork.TypesRepository.GetById(product.TypeId);
            }

            return products;
        }

        public async Task<Product> FindProductById(int id)
        {
            return await unitOfWork.ProductRepository.GetById(id);
        }
    }
}
