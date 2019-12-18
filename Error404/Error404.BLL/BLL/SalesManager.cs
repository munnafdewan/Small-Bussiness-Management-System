using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.Repository.Repository;


namespace Error404.BLL.BLL
{
   public class SalesManager
    {
        SalesRepository _salesRepository = new SalesRepository();
        public bool Add(Sale sale)
        {
            return _salesRepository.Add(sale);
        }

        public List<Sale> GetAllSale()
        {
            return _salesRepository.GetAllSale();
        }

        public List<SaleDetails> GetAllSaleDetails()
        {
            return _salesRepository.GetAllSaleDetails();
        }
        public List<Sale> GetAll()
        {
            return _salesRepository.GetAll();
        }
    }
}
