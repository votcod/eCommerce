using eCommerce.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class ProductRepository : IDataRepository<Product, ProductEditViewModel>, IDataAction
    {
        private readonly DataContext context;
        public ProductRepository(DataContext dataContext) => context = dataContext;

        public async Task<Product> CreateItemAsync(ProductEditViewModel item)
        {
            Product prod = new Product
            {
                ProductId = 0,
                Name = item.Name,
                Price = (decimal)item.Price,
                Description = item.Description,
                CategoryId = item.CategoryId
            };
            if (item.Picture != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(item.Picture.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)item.Picture.Length);
                }
                prod.Picture = imageData;
            }
            context.Products.Add(prod);
            await context.SaveChangesAsync();            
            return prod;
        }

        public async Task<Product> DeleteItemAsync(long id)
        {

            context.Products.Remove(new Product { ProductId = id });
            await context.SaveChangesAsync();
            return new Product();
        }        

        public async Task<Product> EditItemAsync(ProductEditViewModel item)
        {
            Product prod = await FindItemByIdAsync(item.ProductId);
            if (prod != null)
            {
                prod.Name = item.Name;
                prod.Price = (decimal)item.Price;
                prod.Description = item.Description;
                prod.CategoryId = item.CategoryId;
            }

            if (item.Picture != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(item.Picture.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)item.Picture.Length);
                }
                prod.Picture = imageData;
            }
            await context.SaveChangesAsync();
            return prod;
        }

        public async Task<IEnumerable<Product>> FindAsync(string partOfName)
        {
            if (partOfName == null) return await GetAllItemsAsync();

            IEnumerable<Product> products =
                GetAllItemsAsync().Result
                .Where(r =>
                r.Name.Contains(partOfName) ||
                r.Name.ToLower().Contains(partOfName) ||
                r.Name.ToUpper().Contains(partOfName));

            if (products != null) return products;
            else return new List<Product>();
        }

        public async Task<Product> FindItemByIdAsync(long id) => await context.Products
            .Include(r => r.Category).FirstOrDefaultAsync(r => r.ProductId == id);       

        public async Task<IEnumerable<Product>> GetAllItemsAsync() => await context.Products
            .Include(r => r.Category).ToArrayAsync();

        public async Task<IEnumerable<Product>> SortAsync(SortState sortState)
        {
            var products = await GetAllItemsAsync();
            if (sortState == SortState.NameAscending)
                return products.OrderBy(r => r.Name);
            else if (sortState == SortState.NameDescending)
                return products.OrderByDescending(r => r.Name);
            else if (sortState == SortState.PriceAscending)
                return products.OrderBy(r => r.Price);
            else if (sortState == SortState.PriceDescending)
                return products.OrderByDescending(r => r.Price);

            return new List<Product>();
        }
    }
}
