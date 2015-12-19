using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetstProject.Models;

namespace TetstProject.DataAccessLayer
{
    class MyDbContext : DbContext
    {
        public MyDbContext():base("DbConnect")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
  
}
