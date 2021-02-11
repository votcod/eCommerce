using eCommerce.Infrastructure;
using eCommerce.Models;
using eCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private IDataRepository<Product, ProductEditViewModel> repository;

        public CartController(IDataRepository<Product, ProductEditViewModel> repo) 
            => repository = repo;
        
        public ViewResult List()
        {
            return View(GetCart());
        }

        public RedirectToActionResult AddToCart(int productId)
        {
            Product product = repository.GetAllItems()
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                //TempData["Message"] = $"Product {product.Name} has been successfully " +
                //    $"added to the shopping cart";
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("List", "Product");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            Product product = repository.GetAllItems()
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction(nameof(List));
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
