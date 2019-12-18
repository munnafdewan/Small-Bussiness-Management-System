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
    public class SalesReportController: Controller
    {

        PurchaseDetailsManager _purchaseDetailsManager = new PurchaseDetailsManager();
        SaleDetailsManager _saleDetailsManager = new SaleDetailsManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        PurchaseDetails purchaseDetails = new PurchaseDetails();
        SaleDetails salesDetails = new SaleDetails();
      [HttpGet]
        public ActionResult Search()
        {
            SalesReportViewModel salesReportViewModel = new SalesReportViewModel();
            var purchases = _purchaseManager.GetAll();
            var products = _productManager.GetAll();
            var categories = _categoryManager.GetAll();
            var purchaseDetails = _purchaseDetailsManager.GetAll();
            var salesDetails = _saleDetailsManager.GetAll();
            var report = (from sa in salesDetails
                          join Products in products on new { ProductId = sa.ProductId } equals new { ProductId = Products.Id } into Products_join
                          from Products in Products_join.DefaultIfEmpty()
                          join Categories in categories on new { CategoryId = Products.CategoryId } equals new { CategoryId = Categories.Id } into Categories_join
                          from Categories in Categories_join.DefaultIfEmpty()
                          group new { sa.Sale, sa, Products, Categories } by new
                          {
                              sa.Sale.Date,
                              sa.ProductId,
                              Products.Name,
                              Products.Code,
                              Column1 = Categories.Name
                          } into g
                          select new SalesReportViewModel
                          {
                              Date = g.Key.Date,
                              Code = g.Key.Code,
                              Name = g.Key.Name,
                              Category = g.Key.Column1,
                              Soldqty = g.Sum(p => p.sa.Quantity),
                              CP = g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g1
                                select new
                                {
                                    Column1 =(g1.Sum(p => p.UnitPrice) /g.Count())
                                }).First().Column1),
                              SalesPrice = (g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g2
                                select new
                                {
                                    Column1 = (g2.Sum(p => p.MRP) / g.Count())
                                }).First().Column1)),
                              Profit = (g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g3
                                select new
                                {
                                    Column1 = (g3.Sum(p => p.MRP) / g.Count())
                                }).First().Column1) - g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g4
                                select new
                                {
                                    Column1 = (g4.Sum(p => p.UnitPrice) / g.Count())
                                }).First().Column1))
                          }).ToList();

            salesReportViewModel.SalesReport = report;

            return View(salesReportViewModel);
        }
       [HttpPost]
        public ActionResult Search(DateTime? startdate, DateTime? enddate)
        {
            SalesReportViewModel salesReportViewModel = new SalesReportViewModel();
            var purchases = _purchaseManager.GetAll();
            var products = _productManager.GetAll();
            var categories = _categoryManager.GetAll();
            var purchaseDetails = _purchaseDetailsManager.GetAll();
            var salesDetails = _saleDetailsManager.GetAll();
            var report = (from sa in salesDetails
                          join Products in products on new { ProductId = sa.ProductId } equals new { ProductId = Products.Id } into Products_join
                          from Products in Products_join.DefaultIfEmpty()
                          join Categories in categories on new { CategoryId = Products.CategoryId } equals new { CategoryId = Categories.Id } into Categories_join
                          from Categories in Categories_join.DefaultIfEmpty()
                          group new { sa.Sale, sa, Products, Categories } by new
                          {
                              sa.Sale.Date,
                              sa.ProductId,
                              Products.Name,
                              Products.Code,
                              Column1 = Categories.Name
                          } into g
                          where g.Key.Date >=startdate && g.Key.Date<=enddate
                          select new SalesReportViewModel
                          {
                              Date = g.Key.Date,
                              Code = g.Key.Code,
                              Name = g.Key.Name,
                              Category = g.Key.Column1,
                              Soldqty = g.Sum(p => p.sa.Quantity),
                              CP = g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g1
                                select new
                                {
                                    Column1 = (g1.Sum(p => p.UnitPrice) / g.Count())
                                }).First().Column1),
                              SalesPrice = (g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g2
                                select new
                                {
                                    Column1 = (g2.Sum(p => p.MRP) / g.Count())
                                }).First().Column1)),
                              Profit = (g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g3
                                select new
                                {
                                    Column1 = (g3.Sum(p => p.MRP) / g.Count())
                                }).First().Column1) - g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g4
                                select new
                                {
                                    Column1 = (g4.Sum(p => p.UnitPrice) / g.Count())
                                }).First().Column1))
                          }).ToList();

            salesReportViewModel.SalesReport = report;

            return View(salesReportViewModel);
        }

    }
}