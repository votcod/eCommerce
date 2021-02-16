using eCommerce.Models;
using eCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IDataRepository<Category, Category> categoryRepository;
        private readonly IDataRepository<Product, ProductEditViewModel> productRepository;
        public CategoryController(IDataRepository<Category, Category> category, IDataRepository<Product, ProductEditViewModel> product)
        {
            productRepository = product;
            categoryRepository = category;
        }
       

        public async Task<ViewResult> List() => View(await categoryRepository.GetAllItemsAsync());

        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = $"Category {category.Name} has been successfully created";
                await categoryRepository.CreateItemAsync(category);
                return RedirectToAction(nameof(List));
            }
            return View();
        }
        public async Task<IActionResult> Delete(long categoryId)
        {
            if (productRepository.GetAllItemsAsync().Result.Any(r => r.CategoryId == categoryId))
            {
                TempData["Alert"] = "Some products depend on this category. " +
                    "You cann`t delete it before this connection is enable";
                return RedirectToAction(nameof(List));
            }
            Category category = await categoryRepository.DeleteItemAsync(categoryId);
            if (category != null)
            {
                TempData["Message"] = $"Category {category.Name} has been successfully deleted";
            }            
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Edit(long categoryId)
        {
            Category category = await categoryRepository.FindItemByIdAsync(categoryId);
            if (category != null)
            {
                return View("Create", category);
            }
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = $"Category {category.Name} has been successfully changed";
                await categoryRepository.EditItemAsync(category);
                return RedirectToAction(nameof(List));
            }
            return View();
        }
    }
}
