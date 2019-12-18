using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;

namespace Error404.DatabaseContext.DatabaseContext
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public  DbSet<Customer> Customers { set; get; }
       public  DbSet<Supplier> Suppliers { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
	    public DbSet<Sale> Sales { set; get; }
        public DbSet<SaleDetails> SaleDetails { set; get; }
    }
}
