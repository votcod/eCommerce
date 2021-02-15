using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public interface IDataAction
    {
        Task<IEnumerable<Product>> FindAsync(string partOfName);
        Task<IEnumerable<Product>> SortAsync(SortState sortState);

    }
}
