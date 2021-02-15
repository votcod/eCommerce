
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public interface IDataRepository<T, V> where T : class
                                           where V : class
    {
        Task<IEnumerable<T>> GetAllItemsAsync();
        Task<T> CreateItemAsync(V item);
        Task<T> FindItemByIdAsync(long id);
        Task<T> DeleteItemAsync(long id);
        Task<T> EditItemAsync(V item);
    }
}
