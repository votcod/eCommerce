using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;
        public ProductRepository(DataContext dataContext) => context = dataContext;
        public IEnumerable<Product> GetAllProducts() => context.Products.ToArray();        
    }
}
