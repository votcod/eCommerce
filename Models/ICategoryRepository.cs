using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        void DeleteCategory(Category category);
        void EditCategory(Category category);
        IEnumerable<Category> GetCategories();
        Category FindCategoryById(long id);
    }
}
