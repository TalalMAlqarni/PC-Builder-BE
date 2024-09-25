
using Microsoft.EntityFrameworkCore;
using src.Entity;

namespace sda_3_online_Backend_Teamwork.src.Database
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) { }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Entity;

namespace src.Database
{
    public class DatabaseContext : DbContext
    {   
        public DbSet<Category> Category { get; set; }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        
        }
    }
}