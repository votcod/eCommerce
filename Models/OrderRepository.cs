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

        public Order CreateItem(Order item)
        {
            _context.Orders.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Order DeleteItem(long id)
        {
            Order order = FindItemById(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return order;
        }        

        public Order EditItem(Order item)
        {
            _context.Orders.Update(item);
            _context.SaveChanges();
            return item;
        }

        public Order FindItemById(long id) => _context.Orders
            .Include(o => o.Lines).First(o => o.OrderId == id);
       

        public IEnumerable<Order> GetAllItems() => _context.Orders
            .Include(r => r.Lines).ThenInclude(r => r.Product).ToArray();         
    }
}
