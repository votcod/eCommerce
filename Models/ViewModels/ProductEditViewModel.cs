using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models.ViewModels
{
    [ProductValidation]
    public class ProductEditViewModel : Product
    {
        public new IFormFile Picture { get; set; }
    }
}
