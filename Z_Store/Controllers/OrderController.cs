using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Z_Store.Models;

namespace Z_Store.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        ZDBContext zDB { get; }
        Cart cart { get; }
        public OrderController(ZDBContext _zDB, Cart _cart)
        {
            zDB = _zDB;
            cart = _cart;
        }


        public IActionResult Checkout()
        {
            var items = cart.getall();
            cart.cartItems = items;
            return View(cart);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartitems = cart.getall();
            cart.cartItems = cartitems;
            if(cart.cartItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart Empty");
            }
            if (ModelState.IsValid)
            {
                Createorder(order);
                cart.ClearAllCart();
                return View("CheckoutComplete");
            }
            return View(order);

        }

        public void Createorder(Order order)
        {
            order.Dateorder = DateTime.Now;
            var cartitems = cart.cartItems;
            foreach(var i in cartitems)
            {
                var orderitem = new OrderItems
                {
                    Quantity = i.Quantity,
                    Bookid = i.Book.Id,
                    OrderId = order.Id,
                    Price = i.Book.Price * i.Quantity
                };
                order.OrderItems.Add(orderitem);
                order.OrderTotal += orderitem.Price;
            }
            zDB.Orders.Add(order);
            zDB.SaveChanges();

        }

    }
}
