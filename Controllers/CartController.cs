﻿using eCommerce.Infrastructure;
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
        private IProductRepository repository;

        public CartController(IProductRepository repo) => repository = repo;
        
        public ViewResult List(string returnUrl)
        {
            return View(new CartListViewModel {             
               Cart = GetCart(),
               ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.GetAllProducts()
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction(nameof(List));
        }

        public IActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.GetAllProducts()
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
