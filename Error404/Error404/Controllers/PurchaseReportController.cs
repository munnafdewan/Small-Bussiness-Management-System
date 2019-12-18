using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Error404.BLL.BLL;
using Error404.Model.Model;
using Error404.Models;


namespace Error404.Controllers
{
    public class PurchaseReportController : Controller
    {
        
       PurchaseDetailsManager _purchaseDetailsManager=new PurchaseDetailsManager();
       SaleDetailsManager _saleDetailsManager=new SaleDetailsManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        PurchaseDetails purchaseDetails=new PurchaseDetails();
        SaleDetails salesDetails=new SaleDetails();
        [HttpGet]
        
        public ActionResult Search()
        {
            PurchaseReportViewModel _purchaseReportViewModel = new PurchaseReportViewModel();
            var purchases = _purchaseManager.GetAll();
            var products = _productManager.GetAll();
            var categories = _categoryManager.GetAll();
            var purchaseDetails = _purchaseDetailsManager.GetAll();
            var salesDetails = _saleDetailsManager.GetAll();
            var report= (from pu in purchaseDetails
                         join Purchases in purchases on new { PurchaseId = pu.PurchaseId } equals new { PurchaseId = Purchases.Id } into Purchases_join
                         from Purchases in Purchases_join.DefaultIfEmpty()
                         join Products in products on new { ProductId = pu.ProductId } equals new { ProductId = Products.Id } into Products_join
                         from Products in Products_join.DefaultIfEmpty()
                         join Categories in categories on new { CategoryId = Products.CategoryId } equals new { CategoryId = Categories.Id } into Categories_join
                         from Categories in Categories_join.DefaultIfEmpty()
                         group new { Purchases, pu, Products, Categories } by new
                         {
                             pu.Purchase.Date,
                             pu.ProductId,
                             Products.Name,
                             Column1 = Categories.Name,
                             Products.Code
                         } into g
                         select new PurchaseReportViewModel
                         {
                             Date = g.Key.Date,
                             Code = g.Key.Code,
                             Name = g.Key.Name,
                             Category = g.Key.Column1,
                             Availableqty = (g.Sum(p => p.pu.Quantity) - 
                             ((from sa in salesDetails
                               where
                                sa.ProductId == g.Key.ProductId
                               group sa by new
                               {
                                   sa.ProductId
                               } into g1
                               select new
                               {
                                   Column1 = g1.Sum(p => p.Quantity)
                               }).First().Column1)),
                             CP = ((g.Sum(p => p.pu.Quantity) - 
                             ((from sa in salesDetails
                               where
                                sa.ProductId == g.Key.ProductId
                               group sa by new
                               {
                                   sa.ProductId
                               } into g2
                               select new
                               {
                                   Column1 = g2.Sum(p => p.Quantity)
                               }).First().Column1)) * g.Sum(p => p.pu.UnitPrice) / g.Count()),
                             SalesPrice = ((g.Sum(p => p.pu.Quantity) - 
                             ((from sa in salesDetails
                               where
                                sa.ProductId == g.Key.ProductId
                               group sa by new
                               {
                                   sa.ProductId
                               } into g3
                               select new
                               {
                                   Column1 = g3.Sum(p => p.Quantity)
                               }).First().Column1)) * g.Sum(p => p.pu.MRP) / g.Count()),
                             Profit = ((g.Sum(p => p.pu.Quantity) - 
                             ((from sa in salesDetails
                               where
                                sa.ProductId == g.Key.ProductId
                               group sa by new
                               {
                                   sa.ProductId
                               } into g4
                               select new
                               {
                                   Column1 = g4.Sum(p => p.Quantity)
                               }).First().Column1)) * g.Sum(p => p.pu.MRP) / g.Count() - (g.Sum(p => p.pu.Quantity) - 
                             ((from sa in salesDetails
                               where
                                sa.ProductId == g.Key.ProductId
                               group sa by new
                               {
                                   sa.ProductId
                               } into g5
                               select new
                               {
                                   Column1 = g5.Sum(p => p.Quantity)
                               }).First().Column1)) * g.Sum(p => p.pu.UnitPrice) / g.Count())
                         }).ToList();
            _purchaseReportViewModel.PurchaseReport=report;
           

            return View(_purchaseReportViewModel);
        }
        [HttpPost]
        public ActionResult Search(DateTime? startdate, DateTime? enddate)
        {
            PurchaseReportViewModel _purchaseReportViewModel = new PurchaseReportViewModel();
            var purchases = _purchaseManager.GetAll();
            var products = _productManager.GetAll();
            var categories = _categoryManager.GetAll();
            var purchaseDetails = _purchaseDetailsManager.GetAll();
            var salesDetails = _saleDetailsManager.GetAll();
            var report = (from pu in purchaseDetails
                          join Purchases in purchases on new { PurchaseId = pu.PurchaseId } equals new { PurchaseId = Purchases.Id } into Purchases_join
                          from Purchases in Purchases_join.DefaultIfEmpty()
                          join Products in products on new { ProductId = pu.ProductId } equals new { ProductId = Products.Id } into Products_join
                          from Products in Products_join.DefaultIfEmpty()
                          join Categories in categories on new { CategoryId = Products.CategoryId } equals new { CategoryId = Categories.Id } into Categories_join
                          from Categories in Categories_join.DefaultIfEmpty()
                          group new { Purchases, pu, Products, Categories } by new
                          {
                              pu.Purchase.Date,
                              pu.ProductId,
                              Products.Name,
                              Column1 = Categories.Name,
                              Products.Code
                          } into g
                           where g.Key.Date >= startdate && g.Key.Date <= enddate
                          select new PurchaseReportViewModel
                          {
                              Date = g.Key.Date,
                              Code = g.Key.Code,
                              Name = g.Key.Name,
                              Category = g.Key.Column1,
                              Availableqty = (g.Sum(p => p.pu.Quantity) -
                              ((from sa in salesDetails
                                where
                                 sa.ProductId == g.Key.ProductId
                                group sa by new
                                {
                                    sa.ProductId
                                } into g1
                                select new
                                {
                                    Column1 = g1.Sum(p => p.Quantity)
                                }).First().Column1)),
                              CP = ((g.Sum(p => p.pu.Quantity) -
                              ((from sa in salesDetails
                                where
                                 sa.ProductId == g.Key.ProductId
                                group sa by new
                                {
                                    sa.ProductId
                                } into g2
                                select new
                                {
                                    Column1 = g2.Sum(p => p.Quantity)
                                }).First().Column1)) * g.Sum(p => p.pu.UnitPrice) / g.Count()),
                              SalesPrice = ((g.Sum(p => p.pu.Quantity) -
                              ((from sa in salesDetails
                                where
                                 sa.ProductId == g.Key.ProductId
                                group sa by new
                                {
                                    sa.ProductId
                                } into g3
                                select new
                                {
                                    Column1 = g3.Sum(p => p.Quantity)
                                }).First().Column1)) * g.Sum(p => p.pu.MRP) / g.Count()),
                              Profit = ((g.Sum(p => p.pu.Quantity) -
                              ((from sa in salesDetails
                                where
                                 sa.ProductId == g.Key.ProductId
                                group sa by new
                                {
                                    sa.ProductId
                                } into g4
                                select new
                                {
                                    Column1 = g4.Sum(p => p.Quantity)
                                }).First().Column1)) * g.Sum(p => p.pu.MRP) / g.Count() - (g.Sum(p => p.pu.Quantity) -
                              ((from sa in salesDetails
                                where
                                 sa.ProductId == g.Key.ProductId
                                group sa by new
                                {
                                    sa.ProductId
                                } into g5
                                select new
                                {
                                    Column1 = g5.Sum(p => p.Quantity)
                                }).First().Column1)) * g.Sum(p => p.pu.UnitPrice) / g.Count())
                          }).ToList();
            _purchaseReportViewModel.PurchaseReport = report;


            return View(_purchaseReportViewModel);
        }

    }
}