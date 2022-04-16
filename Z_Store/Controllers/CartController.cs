using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Z_Store.Models;

namespace Z_Store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        ZDBContext zDB { get; }
         Cart cart { get; }

        public CartController(ZDBContext _zDB,Cart _cart)
        {
            zDB = _zDB;
            cart = _cart;
        }

        public IActionResult Index()
        {
            var items = cart.getall();
            cart.cartItems = items;
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var selected = zDB.Books.FirstOrDefault(x => x.Id == id);
            if (selected != null)
            {
                cart.addcart(1, selected);
            }
            return RedirectToAction("index", "Store");
        }


        public IActionResult RemoveFromCart(int id)
        {
            var selected = zDB.Books.FirstOrDefault(x => x.Id == id);
            if (selected != null)
            {
                cart.removecart(selected);
            }
            return RedirectToAction("index");
        }

        public IActionResult IncreaseCart(int id)
        {
            var selected = zDB.Books.FirstOrDefault(x => x.Id == id);
            if (selected != null)
            {
                cart.increase(selected);
            }
            return RedirectToAction("index");
        }

        public IActionResult DecreaseCart(int id)
        {
            var selected = zDB.Books.FirstOrDefault(x => x.Id == id);
            if (selected != null)
            {
                cart.reduce(selected);
            }
            return RedirectToAction("index");
        }

        public IActionResult Clear(int id)
        {
            cart.ClearAllCart();
            return RedirectToAction("index");
        }



    }
}
