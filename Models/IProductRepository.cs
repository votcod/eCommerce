using eCommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product CreateProduct(ProductViewModel product);
        Product FindProductById(long id);
        Product DeleteProduct(long id);
        Product EditProduct(ProductViewModel product);
    }
}
