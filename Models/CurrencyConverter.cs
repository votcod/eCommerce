using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class CurrencyConverter
    {
        public decimal USD { get; set; }
        public decimal ConvertToUSD(decimal priceUAN) => priceUAN / USD;

        public decimal EUR { get; set; }
        public decimal ConvertToEUR(decimal priceUAN) => priceUAN / EUR;
    }
}
