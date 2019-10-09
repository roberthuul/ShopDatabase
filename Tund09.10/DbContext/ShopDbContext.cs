using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tund09._10.Models;

namespace Tund09._10
{
    class ShopDbContext : DbContext
    {
        public DbSet<ShoppingCart> ShoppingCarts {get; set;}
        public DbSet<Food> Foods { get; set; }
    }
}
