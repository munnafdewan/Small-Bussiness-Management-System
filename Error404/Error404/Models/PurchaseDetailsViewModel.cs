using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Error404.Model.Model;
using System.Web.Mvc;

namespace Error404.Models
{
    public class PurchaseDetailsViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double AvailableQuantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Remarks { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public double PreviousUnitPrice { get; set; }
        public double PreviousMRP { get; set; }
        public double MRP { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }

        public List<PurchaseDetails> PurchaseDetails { get; set; }
    }
}