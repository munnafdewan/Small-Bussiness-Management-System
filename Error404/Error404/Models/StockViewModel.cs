using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Error404.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Error404.Models
{
    public class StockViewModel
    {
        public StockViewModel()
        {
            StockReport = new List<StockViewModel>();
        }
        public DateTime PurchaseDate { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string Category { set; get; }
        public double ReorderLevel { set; get; }
        public DateTime ExpireDate { set; get; }
        public double OpeningBalance { set; get; }
        public double Inqty { set; get; }
        public double Out { set; get; }
        public double ClosingBalance{ set; get; }
        public List<StockViewModel> StockReport { set; get; }
    }
}