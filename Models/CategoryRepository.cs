using Microsoft.EntityFrameworkCore;
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

        public async Task<Category> CreateItemAsync(Category item)
        {
            item.CategoryId = 0;
            context.Categories.Add(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<Category> DeleteItemAsync(long id)
        {
            context.Categories.Remove(new Category { CategoryId = id });
            await context.SaveChangesAsync();
            return new Category();
        }

        public async Task<Category> EditItemAsync(Category item)
        {
            context.Categories.Update(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<Category> FindItemByIdAsync(long id) => await context.Categories.FirstOrDefaultAsync(r => r.CategoryId == id);
        public async Task<IEnumerable<Category>> GetAllItemsAsync() => await context.Categories.ToListAsync();
    }
}
