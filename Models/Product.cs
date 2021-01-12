using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; }       
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Picture { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
