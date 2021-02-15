using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Components
{
    public class Categories : ViewComponent
    {
        private readonly IDataRepository<Category, Category> categoryRepository;

        public Categories(IDataRepository<Category, Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categories = categoryRepository.GetAllItemsAsync();                         
            return View(categories.Result.Select(r => r.Name).OrderBy(r => r));
        }
    }
}
