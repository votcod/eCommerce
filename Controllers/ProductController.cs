using eCommerce.Models;
using eCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDataRepository<Product, ProductEditViewModel> productRepository;
        private readonly IDataRepository<Category, Category> categoryRepository;
        private readonly IMemoryCache memoryCache;
        public ProductController(
            IDataRepository<Product, ProductEditViewModel> productRepository,
            IDataRepository<Category, Category> categoryRepository, 
            IMemoryCache memoryCache)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.memoryCache = memoryCache;
        }
           
        public ViewResult List(string category)
        {    

            ViewBag.SelectedCategory = category;

            if (!memoryCache.TryGetValue("key_currency", out CurrencyConverter modelConvertor))
            {
                throw new Exception("Data retrieval error");
            }
            ViewBag.Convertor = modelConvertor;            
            return View(productRepository.GetAllItems().Where(p => category == null
            || p.Category.Name == category));
        }
        public IActionResult Info(long productId)
        {
            Product product = productRepository.FindItemById(productId);
            if (product != null)
            {
                return View(product);
            }
            return RedirectToAction(nameof(List));
        }
        public IActionResult Find(string partOfName)
        {
            if (partOfName == null) return RedirectToAction(nameof(List));

            if (!memoryCache.TryGetValue("key_currency", out CurrencyConverter modelConvertor))
            {
                throw new Exception("Data retrieval error");
            }
            ViewBag.Convertor = modelConvertor;

            List<Product> products = productRepository
                .GetAllItems()
                .Where(r => 
                r.Name.Contains(partOfName) || 
                r.Name.ToLower().Contains(partOfName) || 
                r.Name.ToUpper().Contains(partOfName))
                .ToList();
            if (products != null) return View(nameof(List), products);

            return RedirectToAction(nameof(List));
        }
        public IActionResult Sort(SortState sortState = SortState.NameAscending)
        {
            ViewData["SortByName"] = sortState == SortState.NameAscending ? SortState.NameDescending : SortState.NameAscending;
            ViewData["SortByPrice"] = sortState == SortState.PriceAscending ? SortState.PriceDescending : SortState.PriceAscending;

            if (!memoryCache.TryGetValue("key_currency", out CurrencyConverter modelConvertor))
            {
                throw new Exception("Data retrieval error");
            }
            ViewBag.Convertor = modelConvertor;

            if (sortState == SortState.NameAscending) 
                return View(nameof(List), productRepository.GetAllItems().OrderBy(r => r.Name));            
            else if (sortState == SortState.NameDescending) 
                return View(nameof(List), productRepository.GetAllItems().OrderByDescending(r => r.Name));
            else if (sortState == SortState.PriceAscending) 
                return View(nameof(List), productRepository.GetAllItems().OrderBy(r => r.Price));
            else if (sortState == SortState.PriceDescending) 
                return View(nameof(List), productRepository.GetAllItems().OrderByDescending(r => r.Price));

            return RedirectToAction(nameof(List));
        }       
    }
}
