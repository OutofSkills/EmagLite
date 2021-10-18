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

        public void AddProduct(Product product)
        {
            if (product is null)
                throw new Exception("The product object was null.");

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
            await unitOfWork.ProductRepository.Delete(productId);
            unitOfWork.SaveChanges();
        }

        public void EditProduct(Product product)
        {
            if (product is null)
                throw new Exception("The product object was null.");

            unitOfWork.ProductRepository.Update(product);
            unitOfWork.SaveChanges();
        }

        public void BuyProducts()
        {
            //TODO
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await unitOfWork.ProductRepository.GetAll();
        }

        public async Task<Product> FindProductById(int id)
        {
            return await unitOfWork.ProductRepository.GetById(id);
        }
    }
}
