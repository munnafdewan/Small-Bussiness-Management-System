using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Error404.Model.Model;
using Error404.Models;
using System.Web.Mvc;

namespace Error404.Models
{
    public class SaleDetailsViewModel
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
        public List<SelectListItem> ProductSelectListItems { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
        public List<SaleDetails> SaleDetails { get; set; }
    }
}