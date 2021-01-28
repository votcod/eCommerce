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
        private readonly IDataRepository<Product, ProductEditViewModel> productRepository;
        private readonly IDataRepository<Category, Category> categoryRepository;
        public AdminController(
            IDataRepository<Product, ProductEditViewModel> ProductRepository, 
            IDataRepository<Category, Category> CategoryRepository)
        {
            categoryRepository = CategoryRepository;
            productRepository = ProductRepository;
        }           

        public ViewResult List() => View(productRepository.GetAllItems());

        public ViewResult Create()
        {
            ViewBag.Categories = categoryRepository.GetAllItems();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductEditViewModel product)
        {
            ViewBag.Categories = categoryRepository.GetAllItems();
            if (ModelState.IsValid)
            {
                Product prod = productRepository.CreateItem(product);
                TempData["Message"] = $"Product {prod.Name} has been successfully added";
                return RedirectToAction(nameof(List));
            }
           return View();
        }
        public IActionResult Delete(long productId)
        {
            Product prod = productRepository.DeleteItem(productId);
            if (prod != null)
            {
                TempData["Message"] = $"Product {prod.Name} has been successfully deleted";
            }
            return RedirectToAction(nameof(List));
        }
        public IActionResult Edit(long productId)
        {
            ViewBag.Categories = categoryRepository.GetAllItems();
            Product product = productRepository.FindItemById(productId);

            if (product != null)
            {
                return View("Create", product);
            }
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public IActionResult Edit(ProductEditViewModel product)
        {
            if (ModelState.IsValid)
            {
                Product prod = productRepository.EditItem(product);
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
