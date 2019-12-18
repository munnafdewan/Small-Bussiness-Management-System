using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.Repository.Repository;

namespace Error404.BLL.BLL
{
    public class SupplierManager
    {
        SupplierRepository _supplierRepository = new SupplierRepository();
        public bool Add(Supplier supplier)
        {
            return _supplierRepository.Add(supplier);
        }
        public bool Delete(int id)
        {
            return _supplierRepository.Delete(id);
        }
        public bool Update(Supplier supplier)
        {
            return _supplierRepository.Update(supplier);
        }
        public List<Supplier> GetAll()
        {
            return _supplierRepository.GetAll();
        }
        public Supplier GetById(int id)
        {
            return _supplierRepository.GetById(id);
        }
        public string UniqueTest(Supplier supplier)
        {
            return _supplierRepository.UniqueTest(supplier);
        }

    }
}
