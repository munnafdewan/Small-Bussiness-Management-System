using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Error404.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Error404.Models
{
    public class SaleViewModel
    {
        public SaleViewModel()
        {
            SaleDetials = new List<SaleDetails>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public float GrandTotal { get; set; }
        public float Discount { get; set; }
        public float DiscountAmount { get; set; }
        public float PayableAmount { get; set; }
        public double LoyalityPoint { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<SaleDetails> SaleDetials { get; set; }
        public List<SelectListItem> CustomerSelectListItems { get; set; }
        public List<Sale> Sales { get; set; }
    }
}