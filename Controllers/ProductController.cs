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
           
        public async Task<ViewResult> List(string category)
        {
            ViewBag.SelectedCategory = category;         
            ViewBag.Convertor = SetCurrencyConvertor();

            var products = await productRepository.GetAllItemsAsync();

            return View(products.Where(p => category == null
            || p.Category.Name == category));
        }
        public async Task<IActionResult> Info(long productId)
        {
            Product product = await productRepository.FindItemByIdAsync(productId);
            if (product != null) return View(product);
            
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Find(string partOfName)
        {        
            ViewBag.Convertor = SetCurrencyConvertor();
            return View(nameof(List), await dataAction.FindAsync(partOfName));            
        }
        public async Task<IActionResult> Sort(SortState sortState = SortState.NameAscending)
        {
            ViewData["SortByName"] = sortState == SortState.NameAscending ? SortState.NameDescending : SortState.NameAscending;
            ViewData["SortByPrice"] = sortState == SortState.PriceAscending ? SortState.PriceDescending : SortState.PriceAscending;

            ViewBag.Convertor = SetCurrencyConvertor();

            return View(nameof(List), await dataAction.SortAsync(sortState));
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
