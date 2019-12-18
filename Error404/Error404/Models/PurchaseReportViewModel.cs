using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Error404.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Error404.Models
{
    public class PurchaseReportViewModel
    {
        public PurchaseReportViewModel()
        {
            PurchaseReport = new List<PurchaseReportViewModel>();
        }
        public DateTime Date { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string Category { set; get; }
        public double Availableqty { set; get; }
        public double CP { set; get; }
        public double SalesPrice { set; get; }
        public double Profit { set; get; }
        public List<PurchaseReportViewModel> PurchaseReport { set; get; }
        
    }
}