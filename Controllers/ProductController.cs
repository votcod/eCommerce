using eCommerce.Models;
using eCommerce.Models.ViewModels;
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
        private readonly ICategoryRepository categoryRepository;
        public ProductController(IProductRepository ProductRepository, ICategoryRepository CategoryRepository)
        {
            productRepository = ProductRepository;
            categoryRepository = CategoryRepository;

        }
           
        public ViewResult List(int page = 1)
        {
            int pageSize = 3;

            IEnumerable<Product> source = productRepository.GetAllProducts();

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ProductListViewModel viewModel = new ProductListViewModel
            {
                PageViewModel = pageViewModel,
                Products = items                
            };
            return View(viewModel);
        }
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

            if (sortState == SortState.NameAscending) 
                return View(nameof(List), productRepository.GetAllProducts().OrderBy(r => r.Name));            
            else if (sortState == SortState.NameDescending) 
                return View(nameof(List), productRepository.GetAllProducts().OrderByDescending(r => r.Name));
            else if (sortState == SortState.PriceAscending) 
                return View(nameof(List), productRepository.GetAllProducts().OrderBy(r => r.Price));
            else if (sortState == SortState.PriceDescending) 
                return View(nameof(List), productRepository.GetAllProducts().OrderByDescending(r => r.Price));

            return RedirectToAction(nameof(List));
        }
    }
}
