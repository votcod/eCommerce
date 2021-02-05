using eCommerce.Infrastructure;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Components
{
    public class ShoppingCart : ViewComponent
    {        
        public IViewComponentResult Invoke()
        {
            return View(GetCart());            
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
    }
}
