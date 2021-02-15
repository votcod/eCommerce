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

        public async Task<ViewResult> List() => View(await productRepository.GetAllItemsAsync());

        public async Task<ViewResult> Create()
        {
            ViewBag.Categories = await categoryRepository.GetAllItemsAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductEditViewModel product)
        {
            ViewBag.Categories = await categoryRepository.GetAllItemsAsync();
            if (ModelState.IsValid)
            {
                Product prod = await productRepository.CreateItemAsync(product);
                TempData["Message"] = $"Product {prod.Name} has been successfully added";
                return RedirectToAction(nameof(List));
            }
           return View();
        }
        public async Task<IActionResult> Delete(long productId)
        {
            Product prod = await productRepository.DeleteItemAsync(productId);
            if (prod != null)
            {
                TempData["Message"] = $"Product {prod.Name} has been successfully deleted";
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Edit(long productId)
        {
            ViewBag.Categories = await categoryRepository.GetAllItemsAsync();
            Product product = await productRepository.FindItemByIdAsync(productId);

            if (product != null)
            {
                return View("Create", product);
            }
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel product)
        {
            if (ModelState.IsValid)
            {
                Product prod = await productRepository.EditItemAsync(product);
                if (prod != null)
                {
                    TempData["Message"] = $"Product {prod.Name} has been successfully changed";
                }
                return RedirectToAction(nameof(List));
            }
            ViewBag.Categories = await categoryRepository.GetAllItemsAsync();
            return View(nameof(Create), product);
        }
    }
}
