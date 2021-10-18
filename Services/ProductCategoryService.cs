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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductCategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddCategory(Category category)
        {
            if (category is null)
                throw new Exception("The category object was null.");

            unitOfWork.CategoryRepository.Insert(category);
            unitOfWork.SaveChanges();
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            await unitOfWork.CategoryRepository.Delete(categoryId);
            unitOfWork.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            if (category is null)
                throw new Exception("The category object was null.");

            unitOfWork.CategoryRepository.Update(category);
            unitOfWork.SaveChanges();
        }

        public async Task<Category> FindCategoryAsync(int categoryId)
        {
            var category = await unitOfWork.CategoryRepository.GetById(categoryId);

            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await unitOfWork.CategoryRepository.GetAll();
        }
    }
}
