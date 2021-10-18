using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTApi.Services.Intefaces
{
    public interface IProductCategoryService
    {
        void AddCategory(Category category);
        void EditCategory(Category category);
        Task<Category> FindCategoryAsync(int categoryId);
        Task RemoveCategoryAsync(int categoryId);
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}