using eCommerce.Models;
using eCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository productRepository;
        public AdminController(IProductRepository ProductRepository) 
            => productRepository = ProductRepository;

        public ViewResult List() => View(productRepository.GetAllProducts());

        public ViewResult Create() => View();
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                Product prod = productRepository.CreateProduct(product);
                TempData["Message"] = $"Product {prod.Name} has been successfully added";
                return RedirectToAction(nameof(List));
            }
            return View();
        }
        public IActionResult Delete(long productId)
        {
            Product prod = productRepository.DeleteProduct(productId);
            if (prod != null)
            {
                TempData["Message"] = $"Product {prod.Name} has been successfully deleted";
            }
            return RedirectToAction(nameof(List));
        }
        public IActionResult Edit(long productId)
        {
            Product product = productRepository.FindProductById(productId);

            if (product != null)
            {
                return View("Create", product);
            }
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                Product prod = productRepository.EditProduct(product);
                if (prod != null)
                {
                    TempData["Message"] = $"Product {prod.Name} has been successfully changed";
                }
                return RedirectToAction(nameof(List));
            }
            return View();
        }
        


    }
}
