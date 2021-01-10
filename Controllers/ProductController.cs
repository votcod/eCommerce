using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository ProductRepository) 
            => productRepository = ProductRepository;
        public ViewResult List() => View(productRepository.GetAllProducts());
        public IActionResult Info(long productId)
        {
            Product product = productRepository.FindProductById(productId);
            if (product != null)
            {
                return View(product);
            }
            return RedirectToAction(nameof(List));
        }
    }
}
