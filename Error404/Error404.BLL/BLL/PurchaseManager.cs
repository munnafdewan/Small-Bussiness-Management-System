using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Repository.Repository;
using Error404.Model.Model;

namespace Error404.BLL.BLL
{
    public class PurchaseManager
    {
        PurchaseRepository _purchaseRepository = new PurchaseRepository();

        public bool Add(Purchase purchase)
        {
            return _purchaseRepository.Add(purchase);
        }

        public List<PurchaseDetails> GetAll()
        {
            return _purchaseRepository.GetAll();
        }

        public List<Purchase> GetAllPurchase()
        {
            return _purchaseRepository.GetAllPurchase();
        }

        public bool Update(PurchaseDetails purchaseDetails)
        {
            return _purchaseRepository.Update(purchaseDetails);
        }
        public List<PurchaseDetails> GetPurchaseQuantityDetails(int productId, int categoryId)
        {
            return _purchaseRepository.GetPurchaseQuantityDetails(productId, categoryId);
        }
        public PurchaseDetails GetPurchaseDetails(int productId, int categoryId)
        {
            return _purchaseRepository.GetPurchaseDetails(productId, categoryId);
        }
    }
}
