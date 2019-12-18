using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Error404.Model.Model;

namespace Error404.Models
{
    public class PurchaseViewModel
    {
        public PurchaseViewModel()
        {
            PurchaseDetails = new List<PurchaseDetails>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [StringLength(4, MinimumLength = 4, ErrorMessage = "InvoiceNo must 4 digit length..")]
        public string InvoiceNo { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public List<Purchase> Purchases { get; set; }

        public List<PurchaseDetails> PurchaseDetails { get; set; }
        public List<SelectListItem> SupplierSelectListItems { get; set; }
    }
}