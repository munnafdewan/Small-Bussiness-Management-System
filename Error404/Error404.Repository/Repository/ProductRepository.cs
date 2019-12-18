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
   public class ProductRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Product product)
        {
            _dbContext.Products.Add(product);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Product> GetAll()
        {
            var products = _dbContext.Products.Include(c => c.Category).ToList();
            return products;
        }

        public Product GetById(int id)
        {
            Product product = _dbContext.Products.FirstOrDefault(c => c.Id == id);
            return product;
        }

        public bool Update(Product product)
        {
            Product singleProduct = _dbContext.Products.FirstOrDefault(c => c.Id == product.Id);
            if (singleProduct != null)
            {
                singleProduct.Code = product.Code;
                singleProduct.Name = product.Name;
                singleProduct.ReorderLevel = product.ReorderLevel;
                singleProduct.Description = product.Description;
                singleProduct.CategoryId = product.CategoryId;
            }

            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Product product = _dbContext.Products.FirstOrDefault(c => c.Id == id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool ExistProductCode(Product product)
        {
            bool isExistProductCode = false;
            Product prdct = _dbContext.Products.FirstOrDefault(c => c.Code == product.Code);
            if (prdct != null)
            {
                isExistProductCode = true;
            }
            return isExistProductCode;
        }

        public bool ExistProductName(Product product)
        {
            bool isExistProductName = false;
            Product prdct = _dbContext.Products.FirstOrDefault(c => c.Name == product.Name);
            if (prdct != null)
            {
                isExistProductName = true;
            }
            return isExistProductName;
        }
    }
}
