using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public interface IDataAction
    {
        IEnumerable<Product> Find(string partOfName);
        IEnumerable<Product> Sort(SortState sortState);

    }
}
