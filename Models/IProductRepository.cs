using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
    }
}
