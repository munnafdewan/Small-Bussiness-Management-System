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
    public class StockController: Controller
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
            StockViewModel stockViewModel = new StockViewModel();
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
                          select new StockViewModel
                          {
                              PurchaseDate = g.Key.Date,
                              Code = g.Key.Code,
                              Name = g.Key.Name,
                              Category = g.Key.Column1,
                              OpeningBalance = (g.Sum(p => p.pu.Quantity) -
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
                              Inqty = g.Sum(p => p.pu.Quantity),
                              Out =
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
                                }).First().Column1),
                              ClosingBalance = ((g.Sum(p => p.pu.Quantity) -
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
                                }).First().Column1)) + g.Sum(p => p.pu.Quantity) -
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
                                }).First().Column1))
                          }).ToList();

            stockViewModel.StockReport = report;

            return View(stockViewModel);
        }
        [HttpPost]


        public ActionResult Search(string product, string category, DateTime? startdate, DateTime? enddate)
        {
            StockViewModel stockViewModel = new StockViewModel();
            var purchases = _purchaseManager.GetAll();
            var products = _productManager.GetAll();
            var categories = _categoryManager.GetAll();
            var purchaseDetails = _purchaseDetailsManager.GetAll();
            var salesDetails = _saleDetailsManager.GetAll();

            if (product != "" && category != "" && startdate != null && enddate != null)
            {
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
                              where g.Key.Name == product && g.Key.Column1 == category && g.Key.Date >= startdate && g.Key.Date <= enddate
                              select new StockViewModel
                              {
                                  PurchaseDate = g.Key.Date,
                                  Code = g.Key.Code,
                                  Name = g.Key.Name,
                                  Category = g.Key.Column1,
                                  OpeningBalance = (g.Sum(p => p.pu.Quantity) -
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
                                  Inqty = g.Sum(p => p.pu.Quantity),
                                  Out =
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
                                    }).First().Column1),
                                  ClosingBalance = ((g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1)) + g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1))
                              }).ToList();
                stockViewModel.StockReport = report;
            } // no null
            else if (product != "" && category == "" && startdate != null && enddate != null)
            {
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
                              where g.Key.Name == product && g.Key.Date >= startdate && g.Key.Date <= enddate
                              select new StockViewModel
                              {
                                  PurchaseDate = g.Key.Date,
                                  Code = g.Key.Code,
                                  Name = g.Key.Name,
                                  Category = g.Key.Column1,
                                  OpeningBalance = (g.Sum(p => p.pu.Quantity) -
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
                                  Inqty = g.Sum(p => p.pu.Quantity),
                                  Out =
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
                                    }).First().Column1),
                                  ClosingBalance = ((g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1)) + g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1))
                              }).ToList();
                stockViewModel.StockReport = report;
            } //category null
            else if (product == "" && category != "" && startdate != null && enddate != null)
            {
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
                              where g.Key.Column1 == category && g.Key.Date >= startdate && g.Key.Date <= enddate
                              select new StockViewModel
                              {
                                  PurchaseDate = g.Key.Date,
                                  Code = g.Key.Code,
                                  Name = g.Key.Name,
                                  Category = g.Key.Column1,
                                  OpeningBalance = (g.Sum(p => p.pu.Quantity) -
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
                                  Inqty = g.Sum(p => p.pu.Quantity),
                                  Out =
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
                                    }).First().Column1),
                                  ClosingBalance = ((g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1)) + g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1))
                              }).ToList();
                stockViewModel.StockReport = report;
            } //product null
            else if (product == "" && category == "" && startdate != null && enddate != null)
            {
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
                              select new StockViewModel
                              {
                                  PurchaseDate = g.Key.Date,
                                  Code = g.Key.Code,
                                  Name = g.Key.Name,
                                  Category = g.Key.Column1,
                                  OpeningBalance = (g.Sum(p => p.pu.Quantity) -
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
                                  Inqty = g.Sum(p => p.pu.Quantity),
                                  Out =
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
                                    }).First().Column1),
                                  ClosingBalance = ((g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1)) + g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1))
                              }).ToList();
                stockViewModel.StockReport = report;
            }  //product & category null
            else if (product != "" && category == "" && startdate == null && enddate == null)
            {
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
                              where g.Key.Name == product
                              select new StockViewModel
                              {
                                  PurchaseDate = g.Key.Date,
                                  Code = g.Key.Code,
                                  Name = g.Key.Name,
                                  Category = g.Key.Column1,
                                  OpeningBalance = (g.Sum(p => p.pu.Quantity) -
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
                                  Inqty = g.Sum(p => p.pu.Quantity),
                                  Out =
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
                                    }).First().Column1),
                                  ClosingBalance = ((g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1)) + g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1))
                              }).ToList();
                stockViewModel.StockReport = report;
            }  //product not null
            else if (product == "" && category != "" && startdate == null && enddate == null)
            {
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
                              where g.Key.Column1 == category
                              select new StockViewModel
                              {
                                  PurchaseDate = g.Key.Date,
                                  Code = g.Key.Code,
                                  Name = g.Key.Name,
                                  Category = g.Key.Column1,
                                  OpeningBalance = (g.Sum(p => p.pu.Quantity) -
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
                                  Inqty = g.Sum(p => p.pu.Quantity),
                                  Out =
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
                                    }).First().Column1),
                                  ClosingBalance = ((g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1)) + g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1))
                              }).ToList();
                stockViewModel.StockReport = report;
            }  //Category not null
            else
            {
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
                              select new StockViewModel
                              {
                                  PurchaseDate = g.Key.Date,
                                  Code = g.Key.Code,
                                  Name = g.Key.Name,
                                  Category = g.Key.Column1,
                                  OpeningBalance = (g.Sum(p => p.pu.Quantity) -
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
                                  Inqty = g.Sum(p => p.pu.Quantity),
                                  Out =
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
                                    }).First().Column1),
                                  ClosingBalance = ((g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1)) + g.Sum(p => p.pu.Quantity) -
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
                                    }).First().Column1))
                              }).ToList();
                stockViewModel.StockReport = report;
            }

            return View(stockViewModel);
        }

    }
}