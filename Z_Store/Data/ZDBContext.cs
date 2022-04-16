using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Z_Store.Models;

namespace Z_Store.Models
{
        public partial class ZDBContext : IdentityDbContext<DefaultUser>
        {
            public ZDBContext()
            {
            }

            public ZDBContext(DbContextOptions options)
                : base(options)
            {
            }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<CartItems> Cartitems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=BookStore;Integrated Security=True");
            }

        }
    }
