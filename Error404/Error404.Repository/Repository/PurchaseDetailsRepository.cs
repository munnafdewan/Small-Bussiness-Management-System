using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.DatabaseContext.DatabaseContext;

namespace Error404.Repository.Repository
{
   public  class PurchaseDetailsRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public List<PurchaseDetails> GetAll()
        {
            return _dbContext.PurchaseDetails.Include(c=>c.Purchase).Include(c=>c.Product).Include(c=>c.Product.Category).ToList();
        }
    }
}
