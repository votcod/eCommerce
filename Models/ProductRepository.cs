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

        public Product CreateItem(ProductEditViewModel item)
        {
            Product prod = new Product
            {
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
            context.SaveChanges();
            return prod;
        }

        public Product DeleteItem(long id)
        {
            Product prod = FindItemById(id);
            context.Products.Remove(prod);
            context.SaveChanges();
            return prod;
        }        

        public Product EditItem(ProductEditViewModel item)
        {
            Product prod = FindItemById(item.ProductId);
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
            context.SaveChanges();
            return prod;
        }

        public IEnumerable<Product> Find(string partOfName)
        {
            if (partOfName == null) return GetAllItems();

            IEnumerable<Product> products =
                GetAllItems()
                .Where(r =>
                r.Name.Contains(partOfName) ||
                r.Name.ToLower().Contains(partOfName) ||
                r.Name.ToUpper().Contains(partOfName));

            if (products != null) return products;
            else return new List<Product>();
        }

        public Product FindItemById(long id) => context.Products
            .Include(r => r.Category).FirstOrDefault(r => r.ProductId == id);       

        public IEnumerable<Product> GetAllItems() => context.Products
            .Include(r => r.Category).ToArray();

        public IEnumerable<Product> Sort(SortState sortState)
        {
            if (sortState == SortState.NameAscending)
                return GetAllItems().OrderBy(r => r.Name);
            else if (sortState == SortState.NameDescending)
                return GetAllItems().OrderByDescending(r => r.Name);
            else if (sortState == SortState.PriceAscending)
                return GetAllItems().OrderBy(r => r.Price);
            else if (sortState == SortState.PriceDescending)
                return GetAllItems().OrderByDescending(r => r.Price);

            return new List<Product>();
        }
    }
}
