using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;
        public CategoryRepository(DataContext dataContext) 
            => context = dataContext;

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public Category FindCategoryById(long id)
            => context.Categories.FirstOrDefault(r => r.CategoryId == id);        

        public IEnumerable<Category> GetCategories() 
            => context.Categories.ToArray();       
    }
}
