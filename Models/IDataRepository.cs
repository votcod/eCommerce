
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public interface IDataRepository<T, V> where T : class
                                           where V : class
    {
        IEnumerable<T> GetAllItems();
        T CreateItem(V item);
        T FindItemById(long id);
        T DeleteItem(long id);
        T EditItem(V item);
    }
}
