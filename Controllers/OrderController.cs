using eCommerce.Infrastructure;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDataRepository<Order, Order> orderRepository;

        public OrderController(IDataRepository<Order, Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<ViewResult> List() => 
            View(await orderRepository.GetAllItemsAsync());
       
        public async Task<ViewResult> Edit(long orderId) => 
            View(nameof(Checkout), await orderRepository.FindItemByIdAsync(orderId));

        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {
            await orderRepository.EditItemAsync(order);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> MarkShipped(long orderId)
        {
            Order order = await orderRepository.FindItemByIdAsync(orderId);
            if (order != null)
            {
                order.IsShipped = true;
                await orderRepository.EditItemAsync(order);
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(long orderId)
        {
            await orderRepository.DeleteItemAsync(orderId);
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout() => View();
        
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {               
                order.Lines = GetCart().Lines.Select(r => new CartLine
                {
                    ProductId = r.ProductId,
                    Quantity = r.Quantity
                    
                }).ToArray();
                await orderRepository.CreateItemAsync(order);
                SaveCart(new Cart());
                return RedirectToAction(nameof(Completed));
            }
            return View();
        }
        public IActionResult Completed() => View();
        private Cart GetCart() =>
            HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        private void SaveCart(Cart cart) => HttpContext.Session.SetJson("Cart", cart);        
    }
}
