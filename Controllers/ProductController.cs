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
        public IActionResult Find(string partOfName)
        {
            if (partOfName == null) return RedirectToAction(nameof(List));
            
            List<Product> products = productRepository
                .GetAllProducts()
                .Where(r => r.Name.Contains(partOfName) || 
                r.Name.ToLower().Contains(partOfName))
                .ToList();
            if (products != null) return View(nameof(List), products);

            return RedirectToAction(nameof(List));
        }
    }
}
