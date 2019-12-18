using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.Repository.Repository;

namespace Error404.BLL.BLL
{
    public class ProductManager
    {
        ProductRepository _productRepository = new ProductRepository();
        public bool Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public bool Update(Product product)
        {
            return _productRepository.Update(product);
        }
        public bool Delete(int id)
        {
            return _productRepository.Delete(id);
        }
        public bool ExistProductCode(Product product)
        {
            return _productRepository.ExistProductCode(product);
        }

        public bool ExistProductName(Product product)
        {
            return _productRepository.ExistProductName(product);
        }
    }
}
