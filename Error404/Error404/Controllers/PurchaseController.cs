using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Error404.Model.Model;
using Error404.Models;
using Error404.BLL.BLL;
using AutoMapper;

namespace Error404.Controllers
{
    public class PurchaseController : Controller
    {
        PurchaseManager _purchaseManager = new PurchaseManager();
        SupplierManager _supplierManager = new SupplierManager();
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        //PurchaseView1 _purchaseView1 = new PurchaseView1();
        PurchaseQtyManager _purchaseQtyManager = new PurchaseQtyManager();

        public ActionResult AddPurchase()
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            PurchaseDetailsViewModel purchaseDetailsViewModel = new PurchaseDetailsViewModel();

            purchaseViewModel.SupplierSelectListItems = _supplierManager
                                                        .GetAll()
                                                        .Select(c => new SelectListItem()
                                                        {
                                                            Value = c.Id.ToString(),
                                                            Text = c.Name
                                                        }).ToList();
            purchaseDetailsViewModel.CategorySelectListItems = _categoryManager
                                                       .GetAll()
                                                       .Select(c => new SelectListItem()
                                                       {
                                                           Value = c.Id.ToString(),
                                                           Text = c.Name
                                                       }).ToList();
            ViewBag.CategoryId = purchaseDetailsViewModel.CategorySelectListItems;

            purchaseViewModel.Purchases = _purchaseManager.GetAllPurchase();
            return View(purchaseViewModel);
        }


        [HttpPost]
        public ActionResult AddPurchase(PurchaseViewModel purchaseViewModel)
        {
            Purchase purchase = Mapper.Map<Purchase>(purchaseViewModel);
            _purchaseManager.Add(purchase);
            PurchaseDetailsViewModel purchaseDetailsViewModel = new PurchaseDetailsViewModel();
            purchaseViewModel.SupplierSelectListItems = _supplierManager
                                                       .GetAll()
                                                       .Select(c => new SelectListItem()
                                                       {
                                                           Value = c.Id.ToString(),
                                                           Text = c.Name
                                                       }).ToList();
            purchaseDetailsViewModel.CategorySelectListItems = _categoryManager
                                                       .GetAll()
                                                       .Select(c => new SelectListItem()
                                                       {
                                                           Value = c.Id.ToString(),
                                                           Text = c.Name
                                                       }).ToList();
            ViewBag.CategoryId = purchaseDetailsViewModel.CategorySelectListItems;
            purchaseViewModel.Purchases = _purchaseManager.GetAllPurchase();

            return View(purchaseViewModel);
        }


        public JsonResult GetProductByCategoryId(int? categoryId)
        {
            var productList = _productManager.GetAll().Where(c => c.CategoryId == categoryId).ToList();
            var products = from p in productList select (new { p.Id, p.Name });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCodeByProductId(int? productId)
        {
            var productList = _productManager.GetAll().Where(c => c.Id == productId).ToList();
            var products = from p in productList select (new { p.Code });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPreviousUnitPriceByProductId(int? productId)
        {
            var purchsaeList = _purchaseManager.GetAll().OrderByDescending(c => c.ProductId == productId).ToList().FirstOrDefault();
            //var previousUnitPrice = from purchsaeList select(new { p.PreviousUnitPrice });
            return Json(purchsaeList, JsonRequestBehavior.AllowGet);
        }


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


        public ActionResult Index()
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            purchaseViewModel.Purchases = _purchaseManager.GetAllPurchase();
            return View(purchaseViewModel);
        }


        //Show Purchase Details
        public ActionResult PurchaseDetails(int id)
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            purchaseViewModel.Purchases = _purchaseManager.GetAllPurchase().Where(c => c.Id == id).ToList();
            ViewBag.Category = _categoryManager.GetAll();

            PurchaseDetailsViewModel purchaseDetailsViewModel = new PurchaseDetailsViewModel();
            purchaseDetailsViewModel.PurchaseDetails = _purchaseManager.GetAll().Where(c => c.PurchaseId == id).ToList();
            ViewBag.Details = purchaseDetailsViewModel.PurchaseDetails;
            return View(purchaseViewModel);
        }

        public ActionResult EditPurchaseDetails(int id)
        {
            PurchaseDetailsViewModel purchaseDetailsViewModel = new PurchaseDetailsViewModel();
            purchaseDetailsViewModel.PurchaseDetails = _purchaseManager.GetAll().Where(c => c.Id == id).ToList();

            //PurchaseDetailsViewModel purchaseDetailsViewModel = new PurchaseDetailsViewModel();
            purchaseDetailsViewModel.CategorySelectListItems = _categoryManager
                                                       .GetAll()
                                                       .Select(c => new SelectListItem()
                                                       {
                                                           Value = c.Id.ToString(),
                                                           Text = c.Name
                                                       }).ToList();
            ViewBag.CategoryId = purchaseDetailsViewModel.CategorySelectListItems;
            return View(purchaseDetailsViewModel);
        }

        [HttpPost]
        public ActionResult EditPurchaseDetails(PurchaseDetailsViewModel purchaseDetailsViewModel)
        {
            string message = "";

            PurchaseDetails purchaseDetails = Mapper.Map<PurchaseDetails>(purchaseDetailsViewModel);

            purchaseDetailsViewModel.CategorySelectListItems = _categoryManager
                                                   .GetAll()
                                                   .Select(c => new SelectListItem()
                                                   {
                                                       Value = c.Id.ToString(),
                                                       Text = c.Name
                                                   }).ToList();
            ViewBag.CategoryId = purchaseDetailsViewModel.CategorySelectListItems;


            if (_purchaseManager.Update(purchaseDetails))
            {
                message = "Updated Successfully..";
            }
            else
            {
                message = "No Change Your Update Information";
            }

            purchaseDetailsViewModel.PurchaseDetails = _purchaseManager.GetAll().Where(c => c.Id == purchaseDetailsViewModel.Id).ToList();
            ViewBag.Message = message;
            return View(purchaseDetailsViewModel);
        }

        public JsonResult SearchSupplier(string SearchData)
        {
            List<Purchase> suppliers = _purchaseManager.GetAllPurchase();
            var date = suppliers.Where(c => c.Date.ToString().Contains(SearchData)).ToList();
            var name = suppliers.Where(c => c.Supplier.Name.Contains(SearchData)).ToList();
            var invoiceNo = suppliers.Where(c => c.InvoiceNo.Contains(SearchData)).ToList();
            if (date.Count() != 0)
            {
                return Json(date, JsonRequestBehavior.AllowGet);
            }

            if (name.Count() != 0)
            {
                return Json(name, JsonRequestBehavior.AllowGet);
            }

            if (invoiceNo.Count() != 0)
            {
                return Json(invoiceNo, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        //Search//
        //public ActionResult Index(string Searchstring)
        //{
        //    PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
        //    var purchases = _purchaseManager.GetAllPurchase();

        //    if (!string.IsNullOrEmpty(Searchstring))
        //    {
        //        purchases = purchases.Where(c => c.Date.ToString().Contains(Searchstring) || c.Supplier.Name.Contains(Searchstring) || c.InvoiceNo.Contains(Searchstring)).ToList();
        //    }
        //    purchaseViewModel.Purchases = purchases;
        //    return View(purchaseViewModel);
        //}
        public ActionResult GetProductDetailsById(int productId, int categoryId)
        {
            var product = _productManager.GetById(productId);
            var purchaseDetails = _purchaseManager.GetPurchaseDetails(productId, categoryId);
            var purchaseQuantityDetails = _purchaseManager.GetPurchaseQuantityDetails(productId, categoryId);


            product.ReorderLevel = (from x in purchaseQuantityDetails select x.Quantity).Sum();


            if (purchaseDetails == null)
            {
                return Json(new { pc = product.Code, aq = product.ReorderLevel, pup = "0", pmrp = "0" });
            }
            else
            {
                return Json(new { pc = product.Code, aq = product.ReorderLevel, pup = purchaseDetails.UnitPrice, pmrp = purchaseDetails.MRP });
            }
        }
    }
}