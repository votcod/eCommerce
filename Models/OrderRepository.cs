using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class OrderRepository : IDataRepository<Order, Order>
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context) => _context = context;       

        public async Task<Order> CreateItemAsync(Order item)
        {
            _context.Orders.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Order> DeleteItemAsync(long id)
        {
            Order order = await FindItemByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }        

        public async Task<Order> EditItemAsync(Order item)
        {
            _context.Orders.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Order> FindItemByIdAsync(long id) => await _context.Orders
            .Include(o => o.Lines).FirstAsync(o => o.OrderId == id);
       

        public async Task<IEnumerable<Order>> GetAllItemsAsync() =>  await _context.Orders
            .Include(r => r.Lines).ThenInclude(r => r.Product).ToArrayAsync();         
    }
}
