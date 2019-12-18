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
    public class SupplierController : Controller
    {
        SupplierManager _supplierManager = new SupplierManager();
        Supplier supplier = new Supplier();
        [HttpGet]
        public ActionResult Index()
        {
            SupplierViewModel _supplierViewModel = new SupplierViewModel();
            _supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(_supplierViewModel);
        }
        public ActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Add(SupplierViewModel supplierViewModel)
        {

            string message = "";

            if (ModelState.IsValid)
            {
                Supplier supplier= Mapper.Map<Supplier>(supplierViewModel);
                string errMsg = _supplierManager.UniqueTest(supplier);

                if (errMsg == "")
                {
                    if (_supplierManager.Add(supplier))
                    {
                        message = "Saved";
                    }
                    else
                    {
                        message = "not saved";
                    }
                }
                else
                {
                    message = errMsg;
                }
            }
            else
            {
                message = "modelstate is invalid";
            }

            ViewBag.Message = message;
            supplierViewModel.Suppliers = _supplierManager.GetAll();

            return View(supplierViewModel);
        }

        [HttpPost]
        public ActionResult Index(string Searchstring)
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();
            var suppliers = _supplierManager.GetAll();

            if (!string.IsNullOrEmpty(Searchstring))
            {
                suppliers = suppliers.Where(c => c.Code.Contains(Searchstring) || c.Name.Contains(Searchstring) || c.Email.Contains(Searchstring)).ToList();
            }
            supplierViewModel.Suppliers = suppliers;
            return View(supplierViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Supplier supplier = _supplierManager.GetById(id);

            SupplierViewModel supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            return View(supplierViewModel);
        }
        [HttpPost]
        public ActionResult Edit(SupplierViewModel supplierViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);

                if (_supplierManager.Update(supplier))
                {
                    message = "Updated";
                }
                else
                {
                    message = "Not Updated";
                }
            }
            else
            {
                message = "ModelState Failed";
            }

            ViewBag.Message = message;
            supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }
    }
}