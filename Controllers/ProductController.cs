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
        private readonly IDataAction dataAction;
        public ProductController(
            IDataRepository<Product, ProductEditViewModel> productRepository,
            IDataRepository<Category, Category> categoryRepository, 
            IMemoryCache memoryCache,
            IDataAction dataAction)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.memoryCache = memoryCache;
            this.dataAction = dataAction;
        }
           
        public ViewResult List(string category)
        {
            ViewBag.SelectedCategory = category;         
            ViewBag.Convertor = SetCurrencyConvertor();
            
            return View(productRepository.GetAllItems().Where(p => category == null
            || p.Category.Name == category));
        }
        public IActionResult Info(long productId)
        {
            Product product = productRepository.FindItemById(productId);
            if (product != null) return View(product);
            
            return RedirectToAction(nameof(List));
        }
        public IActionResult Find(string partOfName)
        {        
            ViewBag.Convertor = SetCurrencyConvertor();
            return View(nameof(List), dataAction.Find(partOfName));            
        }
        public IActionResult Sort(SortState sortState = SortState.NameAscending)
        {
            ViewData["SortByName"] = sortState == SortState.NameAscending ? SortState.NameDescending : SortState.NameAscending;
            ViewData["SortByPrice"] = sortState == SortState.PriceAscending ? SortState.PriceDescending : SortState.PriceAscending;

            ViewBag.Convertor = SetCurrencyConvertor();
            return View(nameof(List), dataAction.Sort(sortState));
        }
        private CurrencyConverter SetCurrencyConvertor()
        {
            if (!memoryCache.TryGetValue("key_currency", out CurrencyConverter modelConvertor))
            {
                throw new Exception("Data retrieval error");
            }
            return modelConvertor;
        }
    }
}
