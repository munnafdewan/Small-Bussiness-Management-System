using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.DatabaseContext.DatabaseContext;
using System.Data.Entity;

namespace Error404.Repository.Repository
{
    public class SalesRepository
    {
        ProjectDbContext dbContext = new ProjectDbContext();
        public bool Add(Sale sales)
        {
            dbContext.Sales.Add(sales);
            //dbContext.SaveChanges();

            return dbContext.SaveChanges() > 0;
        }
        public List<Sale> GetAllSale()
        {
            return dbContext.Sales.ToList();
        }

        public List<SaleDetails> GetAllSaleDetails()
        {
            //return _dbContext.SaleDetials.ToList();
            //var purchaseDetails = _dbContext.PurchaseDetails.Include(p => p.Product).ToList();
            var saleDetials = dbContext.SaleDetails.Include(p => p.Product).ToList();
            return saleDetials;
        }
        public List<Sale> GetAll()
        {
            var sales = dbContext.Sales.Include(c => c.Customer).ToList();

            return sales;
        }
    }
}
