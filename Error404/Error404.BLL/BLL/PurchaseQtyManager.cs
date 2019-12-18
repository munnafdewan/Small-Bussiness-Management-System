using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Repository.Repository;
using Error404.Model.Model;

namespace Error404.BLL.BLL
{
    public class PurchaseQtyManager
    {
        PurchaseQtyRepository _purchaseQtyRepository = new PurchaseQtyRepository();
        public double GetAvailableProduct(int productId)
        {
            return _purchaseQtyRepository.GetAvailableProduct(productId);
        }
        public double GetSaleAvailableProduct(int productId)
        {
            return _purchaseQtyRepository.GetSaleAvailableProduct(productId);
        }
    }
}
