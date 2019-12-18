using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.DatabaseContext.DatabaseContext;

namespace Error404.Repository.Repository
{
    public class SalesDetailsRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public List<SaleDetails> GetAll()
        {
            return _dbContext.SaleDetails.Include(c=>c.Sale).Include(c => c.Product).Include(c => c.Product.Category).ToList();
        }
    }
}
