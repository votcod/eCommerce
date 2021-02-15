using eCommerce.Infrastructure;
using eCommerce.Models;
using eCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


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

        public async Task<RedirectToActionResult> AddToCart(int productId)
        {
            var products = await repository.GetAllItemsAsync();
            Product product = products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {               
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("List", "Product");
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var products = await repository.GetAllItemsAsync();
            Product product = products
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
