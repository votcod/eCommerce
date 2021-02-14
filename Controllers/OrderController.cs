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
        public ViewResult List() => View(orderRepository.GetAllItems());
       
        public ViewResult Edit(long orderId) => View(nameof(Checkout), orderRepository.FindItemById(orderId));

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            orderRepository.EditItem(order);
            return RedirectToAction(nameof(List));
        }
        public IActionResult MarkShipped(long orderId)
        {
            Order order = orderRepository.GetAllItems()
                .FirstOrDefault(r => r.OrderId == orderId);
            if (order != null)
            {
                order.IsShipped = true;
                orderRepository.EditItem(order);
            }
            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(long orderId)
        {
            orderRepository.DeleteItem(orderId);
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
                orderRepository.CreateItem(order);
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
