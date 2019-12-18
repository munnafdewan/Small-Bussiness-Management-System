using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using Error404.BLL.BLL;
using Error404.Model.Model;
using Error404.Repository.Repository;
using Error404.Models;
using AutoMapper;


namespace Error404.Controllers
{
    public class SalesController : Controller
    {

        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        SalesManager _salesManager = new SalesManager();
        CustomerManager _customerManager = new CustomerManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        PurchaseQtyManager _purchaseQtyManager = new PurchaseQtyManager();



        public ActionResult Index()
        {
            SaleViewModel saleViewModel = new SaleViewModel();
            saleViewModel.Sales = _salesManager.GetAll();
            return View(saleViewModel);
        }
        public ActionResult AddSales()
        {
            SaleDetailsViewModel saleDetialsViewModel = new SaleDetailsViewModel();
            SaleViewModel saleViewModel = new SaleViewModel();

            saleViewModel.CustomerSelectListItems = _customerManager
                .GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();


            saleDetialsViewModel.CategorySelectListItems = _categoryManager.GetAll().Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.CategoryId = saleDetialsViewModel.CategorySelectListItems;
            saleViewModel.Sales = _salesManager.GetAll();

            return View(saleViewModel);
        }
        [HttpPost]
        public ActionResult AddSales(SaleViewModel saleViewModel)
        {
            SaleDetailsViewModel saleDetialsViewModel = new SaleDetailsViewModel();
            string message = "";
            if (ModelState.IsValid)
            {
                var resetLoyaltyPoint = saleViewModel.LoyalityPoint - (saleViewModel.LoyalityPoint / 10);
                var newLoyaltyPoint = Convert.ToInt16(resetLoyaltyPoint + ((saleViewModel.GrandTotal) / 1000));
                var customer = _customerManager.GetById(saleViewModel.CustomerId);
                customer.Loyality = newLoyaltyPoint;
                _customerManager.Update(customer);


                Sale sale = Mapper.Map<Sale>(saleViewModel);
                _salesManager.Add(sale);
            }
            else
            {
                message = "modelstate is invalid";
            }


            saleViewModel.CustomerSelectListItems = _customerManager
                                                        .GetAll()
                                                        .Select(c => new SelectListItem()
                                                        {
                                                            Value = c.Id.ToString(),
                                                            Text = c.Name
                                                        }).ToList();

            saleDetialsViewModel.CategorySelectListItems = _categoryManager.GetAll().Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.CategoryId = saleDetialsViewModel.CategorySelectListItems;
            saleViewModel.Sales = _salesManager.GetAll();
            return View(saleViewModel);
        }
        public JsonResult GetProductByCategoryId(int? categoryId)
        {
            var productList = _productManager.GetAll().Where(c => c.CategoryId == categoryId);
            var products = from p in productList select (new { p.Id, p.Name });
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAvlQuntyByProduct(int? productId)
        {
            var purchsaeList = _purchaseManager.GetAll().OrderByDescending(c => c.ProductId == productId).ToList().FirstOrDefault();
            //var purchsaeList = _purchaseReportManager.GetAvailableQtyByProductIdFrmPurchase(productId);
            return Json(purchsaeList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLoyalityPointByCustomer(int? customerId)
        {
            var customerlist = _customerManager.GetAll().OrderByDescending(c => c.Id == customerId).ToList().FirstOrDefault();

            return Json(customerlist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMRPByPurchaseDetials(int? customerId)
        {
            var customerlist = _customerManager.GetAll().OrderByDescending(c => c.Id == customerId).ToList().FirstOrDefault();

            return Json(customerlist, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult GetAvailableQtyByProductIdFrmPurchase(int productId)
        //{
        //    var getAvailableProduct = _purchaseReportManager.GetAvailableQtyByProductIdFrmPurchase(productId);
        //    //var getSaleAvailableProduct = _purchaseReportManager.GetSaleAvailableProduct(productId);

        //    //if (getSaleAvailableProduct > 0)
        //    //{
        //    //    getAvailableProduct = getAvailableProduct - getSaleAvailableProduct;
        //    //}

        //    return Json(getAvailableProduct, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetAvailableQtyByProductId(int productId)
        {
            var getAvailableProduct = _purchaseQtyManager.GetAvailableProduct(productId);
            var getSaleAvailableProduct = _purchaseQtyManager.GetSaleAvailableProduct(productId);

            if (getSaleAvailableProduct > 0)
            {
                getAvailableProduct = getAvailableProduct - getSaleAvailableProduct;
            }

            return Json(getAvailableProduct, JsonRequestBehavior.AllowGet);
        }



        public ActionResult CustomerDetails()
        {
            SaleViewModel saleViewModel = new SaleViewModel();
            saleViewModel.Sales = _salesManager.GetAll();
            return View(saleViewModel);
        }
        // Sales Detials
        public ActionResult SalesDetails(int id)
        {
            //PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            SaleViewModel saleViewModel = new SaleViewModel();

            //purchaseViewModel.Purchases = _purchaseManager.GetAllPurchase().Where(c => c.Id == id).ToList();
            saleViewModel.Sales = _salesManager.GetAll().Where(c => c.Id == id).ToList();
            ViewBag.Category = _categoryManager.GetAll();

            PurchaseDetailsViewModel purchaseDetailsViewModel = new PurchaseDetailsViewModel();
            SaleDetailsViewModel saleDetialsViewModel = new SaleDetailsViewModel();

            //purchaseDetailsViewModel.PurchaseDetails = _purchaseManager.GetAll().Where(c => c.PurchaseId == id).ToList();
            saleDetialsViewModel.SaleDetails = _salesManager.GetAllSaleDetails().Where(c => c.SaleId == id).ToList();
            ViewBag.Details = saleDetialsViewModel.SaleDetails;
            return View(saleViewModel);
        }

        public JsonResult SearchCustomer(string SearchData)
        {
            List<Sale> customers = _salesManager.GetAllSale();
            var date = customers.Where(c => c.Date.ToString().Contains(SearchData)).ToList();
            var name = customers.Where(c => c.Customer.Name.Contains(SearchData)).ToList();          
            if (date.Count() != 0)
            {
                return Json(date, JsonRequestBehavior.AllowGet);
            }

            if (name.Count() != 0)
            {
                return Json(name, JsonRequestBehavior.AllowGet);
            }
           
            return Json(null, JsonRequestBehavior.AllowGet);
        }


    }
}
