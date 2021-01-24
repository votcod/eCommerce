using eCommerce.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;
        public ProductRepository(DataContext dataContext) => context = dataContext;

        public Product CreateProduct(ProductEditViewModel product)
        {
            Product prod = new Product
            {
                Name = product.Name,
                Price = (decimal)product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId
            };
            if (product.Picture != null)
            {
                byte[] imageData = null;
                
                using (var binaryReader = new BinaryReader(product.Picture.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)product.Picture.Length);
                }
                prod.Picture = imageData;
            }
            context.Products.Add(prod);
            context.SaveChanges();
            return prod;
        }

        public Product DeleteProduct(long id)
        {
            Product prod = FindProductById(id);
            context.Products.Remove(prod);
            context.SaveChanges();
            return prod;
        }

        public Product EditProduct(ProductEditViewModel product)
        {
            Product prod = FindProductById(product.ProductId);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Price = (decimal)product.Price;
                prod.Description = product.Description;
                prod.CategoryId = product.CategoryId;               
            }
            
            if (product.Picture != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(product.Picture.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)product.Picture.Length);
                }
                prod.Picture = imageData;
            }
            context.SaveChanges();
            return prod;
        }

        public Product FindProductById(long id) 
            => context.Products.Include(r => r.Category).FirstOrDefault(r => r.ProductId == id);        
    

        public IEnumerable<Product> GetAllProducts() => context.Products.Include(r => r.Category).ToArray();        
    }
}
