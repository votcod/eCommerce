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
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public ViewResult List() => View(orderRepository.Orders);
       
        public ViewResult Edit(long orderId) => View(nameof(Checkout), orderRepository.GetOrder(orderId));

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            orderRepository.UpdateOrder(order);
            return RedirectToAction(nameof(List));
        }
        public IActionResult MarkShipped(long orderId)
        {
            Order order = orderRepository.Orders
                .FirstOrDefault(r => r.OrderId == orderId);
            if (order != null)
            {
                order.IsShipped = true;
                orderRepository.UpdateOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(long orderId)
        {
            orderRepository.DeleteOrder(orderRepository.GetOrder(orderId));
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout() => View();
        
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Lines = GetCart().Lines.Select(r => new CartLine
                {
                    ProductId = r.ProductId,
                    Quantity = r.Quantity
                }).ToArray();
                orderRepository.SaveOrder(order);
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
