using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class CategoryRepository : IDataRepository<Category, Category>
    {
        private readonly DataContext context;
        public CategoryRepository(DataContext dataContext)
            => context = dataContext;


        public Category CreateItem(Category item)
        {
            item.CategoryId = 0;
            context.Categories.Add(item);
            context.SaveChanges();
            return item;
        }

        public Category DeleteItem(long id)
        {
            context.Categories.Remove(new Category { CategoryId = id });
            context.SaveChanges();
            return new Category();
        }

        public Category EditItem(Category item)
        {
            context.Categories.Update(item);
            context.SaveChanges();
            return item;
        }

        public Category FindItemById(long id) => context.Categories.FirstOrDefault(r => r.CategoryId == id);
        public IEnumerable<Category> GetAllItems() => context.Categories.ToArray();
    }
}
