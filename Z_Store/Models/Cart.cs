using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace Z_Store.Models
{
    public partial class Cart
    {
        ZDBContext zDB { get; }
        public Cart(ZDBContext _zDB)
        {
            zDB = _zDB;
        }
        public string Id { get; set; }
        public List<CartItems> cartItems { get; set; }


        public static Cart getcartsission(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var con = service.GetService<ZDBContext>();
            string cartid = session.GetString("Id") ?? Guid.NewGuid().ToString();
            session.SetString("Id", cartid);
            return new Cart(con) { Id = cartid };

        }


        public List<CartItems> getall()
        {
            return cartItems ?? (cartItems = zDB.Cartitems.Where(x => x.Cartid == Id).Include(x => x.Book).ToList());
        }

        public CartItems getcartbyid(Book book)
        {
            return zDB.Cartitems.SingleOrDefault(x => x.Book.Id == book.Id && x.Cartid == Id);
        }

        public void addcart(int qua, Book book)
        {
            var cartitem = getcartbyid(book);
            if(cartitem == null)
            {
                cartitem = new CartItems
                {
                    Cartid = Id,
                    Book = book,
                    Quantity = qua
                };
                zDB.Cartitems.Add(cartitem);
            }
            else
            {
                cartitem.Quantity += qua;
            }
            zDB.SaveChanges();
        }

        public int GetNet(int x)
        {
            return (zDB.Cartitems.Where(x => x.Cartid == Id).Select(x => x.Book.Price * x.Quantity).Sum())+x;
        }

        public int gettotal()
        {
            return zDB.Cartitems.Where(x => x.Cartid == Id).Select(x => x.Book.Price * x.Quantity).Sum();
        }

        public int reduce(Book book)
        {
            var cartitems = getcartbyid(book);
            var remainig = 0;
            if (cartitems != null)
            {
                if (cartitems.Quantity > 1)
                {
                    remainig = --cartitems.Quantity;
                }
                else
                {
                    zDB.Cartitems.Remove(cartitems);
                }
            }
            zDB.SaveChanges();
            return remainig;
        }

        public int increase(Book book)
        {
            var cartitems = getcartbyid(book);
            var remainig = 0;
            if (cartitems != null)
            {
                if (cartitems.Quantity > 0)
                {
                    remainig = ++cartitems.Quantity;
                }
            }
            zDB.SaveChanges();
            return remainig;
        }

        public void removecart(Book book)
        {
            var cartitems = getcartbyid(book);
            if (cartitems != null)
            {
                zDB.Cartitems.Remove(cartitems);
            }
            zDB.SaveChanges();
        }

        public void clearcart(Book book)
        {
            var cartitems = zDB.Cartitems.Where(x => x.Cartid == Id);

            zDB.Cartitems.RemoveRange(cartitems);

            zDB.SaveChanges();
        }

        public void ClearAllCart()
        {
            var cartitems = zDB.Cartitems.Where(x => x.Cartid == Id);

            zDB.Cartitems.RemoveRange(cartitems);

            zDB.SaveChanges();
        }

        public int getallqu()
        {
            return zDB.Cartitems.Where(x => x.Cartid == Id).Select(x => x.Quantity).Sum();
        }


    }
}
