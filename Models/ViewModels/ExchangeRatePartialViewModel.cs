using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models.ViewModels
{
    public class ExchangeRatePartialViewModel
    {
        public Product Product { get; set; }
        public CurrencyConverter CurrencyConverter { get; set; }
    }
}
