using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models.ViewModels
{
    public class ProductListViewModel
    {  
        public IEnumerable<Product> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public CurrencyConverter CurrencyConverter { get; set; }
    }
}
