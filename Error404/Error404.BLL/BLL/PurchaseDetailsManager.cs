using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Error404.Model.Model;
using  Error404.Repository.Repository;
namespace Error404.BLL.BLL
{
    public class PurchaseDetailsManager
    {
        PurchaseDetailsRepository _purchaseDetailsRepository=new PurchaseDetailsRepository();

        public List<PurchaseDetails> GetAll()
        {
            return _purchaseDetailsRepository.GetAll();
        }
    }
}
