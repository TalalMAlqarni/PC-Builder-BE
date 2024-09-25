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