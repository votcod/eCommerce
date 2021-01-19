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
        private readonly ICategoryRepository categoryRepository;

        public Categories(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
           
            var categories = categoryRepository
                .GetCategories()
                .Select(r => r.Name)
                .OrderBy(r => r);            
            return View(categories);
        }
    }
}
