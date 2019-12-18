using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using Error404.BLL.BLL;
using Error404.Model.Model;
using Error404.Models;
using AutoMapper;

namespace Error404.Controllers
{
    public class CustomerController : Controller
    {
        CustomerManager _customerManager = new CustomerManager();
        Customer customer = new Customer();
        [HttpGet]
        public ActionResult Index()
        {
            CustomerViewModel _customerViewModel = new CustomerViewModel();
            _customerViewModel.Customers = _customerManager.GetAll();
            return View(_customerViewModel);
        }

        public ActionResult Add()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Add(CustomerViewModel customerViewModel)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                Customer customer = Mapper.Map<Customer>(customerViewModel);
                string errMsg = _customerManager.UniqueTest(customer);

                if (errMsg == "")
                {
                    if (_customerManager.Add(customer))
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
            customerViewModel.Customers = _customerManager.GetAll();

            return View(customerViewModel);
        }

        [HttpPost]
        public ActionResult Index(string Searchstring)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            var customers =_customerManager.GetAll();

            if (!string.IsNullOrEmpty(Searchstring))
            {
                customers = customers.Where(c => c.Code.Contains(Searchstring) || c.Name.Contains(Searchstring) || c.Email.Contains(Searchstring)).ToList();
            }
            customerViewModel.Customers = customers;
            return View(customerViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Customer customer = _customerManager.GetById(id);

            CustomerViewModel customerViewModel = Mapper.Map<CustomerViewModel>(customer);
            return View(customerViewModel);
        }
        [HttpPost]
        public ActionResult Edit(CustomerViewModel customerViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                Customer customer = Mapper.Map<Customer>(customerViewModel);

                if (_customerManager.Update(customer))
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
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
        }
    }
}