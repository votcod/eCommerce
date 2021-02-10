using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^\+38-\d{3}-\d{3}-\d{4}$", ErrorMessage = "The phone number must be in the format: +38-XXX-XXX-XXXX")]
        public string PhoneNumber { get; set; }
        public bool IsShipped { get; set; }
        public IEnumerable<CartLine> Lines { get; set; }
    }
}
