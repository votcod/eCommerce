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
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository category) 
            => categoryRepository = category;

        public ViewResult List() => View(categoryRepository.GetCategories());

        public ViewResult Create() => View();
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.AddCategory(category);
                return RedirectToAction(nameof(List));
            }
            return View();
        }
        public IActionResult Delete(long categoryId)
        {
            Category category = categoryRepository.FindCategoryById(categoryId);
            if (category != null)
            {
                categoryRepository.DeleteCategory(category);
                return RedirectToAction(nameof(List));
            }            
            return RedirectToAction(nameof(List));
        }
        public IActionResult Edit(long categoryId)
        {
            Category category = categoryRepository.FindCategoryById(categoryId);
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
                categoryRepository.EditCategory(category);
                return RedirectToAction(nameof(List));
            }
            return View();
        }

    }
}
