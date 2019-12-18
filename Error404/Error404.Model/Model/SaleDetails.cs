using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Error404.Model.Model
{
   public class SaleDetails
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        public float MRP { get; set; }
        public float TotalMRP { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
