using eCommerce.Models;
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
        public CategoryController(IDataRepository<Category, Category> category) 
            => categoryRepository = category;

        public ViewResult List() => View(categoryRepository.GetAllItems());

        public ViewResult Create() => View();
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.CreateItem(category);
                return RedirectToAction(nameof(List));
            }
            return View();
        }
        public IActionResult Delete(long categoryId)
        {
           Category category = categoryRepository.DeleteItem(categoryId);
            if (category != null)
            {
                return RedirectToAction(nameof(List));
            }            
            return RedirectToAction(nameof(List));
        }
        public IActionResult Edit(long categoryId)
        {
            Category category = categoryRepository.FindItemById(categoryId);
            if (category != null)
            {
                return View("Create", category);
            }
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.EditItem(category);
                return RedirectToAction(nameof(List));
            }
            return View();
        }

    }
}
