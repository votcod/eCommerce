﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models.ViewModels
{
    [ProductValidation]
    public class ProductEditViewModel
    {
        public long ProductId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid name length")]
        public string Name { get; set; }
        public long CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid price length, possible range from 0 to 100000000")]
        public decimal? Price { get; set; }
        public IFormFile Picture { get; set; }
    }
}
