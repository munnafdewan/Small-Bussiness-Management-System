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
    public class CategoryController : Controller
    {


        CategoryManager _categoryManager = new CategoryManager();
        
        public ActionResult Index()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = _categoryManager.GetAll();
            return View(categoryViewModel);
        }

        //***************Add Category*****************
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel categoryViewModel)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                Category category = Mapper.Map<Category>(categoryViewModel);

                categoryViewModel.Categories = _categoryManager.GetAll();
                bool isExistCategoryCode = _categoryManager.ExistCategoryCode(category);
                if (isExistCategoryCode)
                {
                    ViewBag.existDuplicate = "Code is Already Exist..";

                    return View(categoryViewModel);
                }

                bool isExistCategoryName = _categoryManager.ExistCategoryName(category);
                if (isExistCategoryName)
                {
                    ViewBag.existDuplicate = "Name is Already Exist..";

                    return View(categoryViewModel);
                }

                if (_categoryManager.Add(category))
                {
                    message = "Saved Successfully..";
                }
                else
                {
                    message = "Not Saved";
                }
            }
            else
            {
                ViewBag.InvalidModel = "ModelState is invalied!";
            }

            ViewBag.Message = message;
            return View(categoryViewModel);
        }

        //******************Search Category*********************

        [HttpPost]
        public ActionResult Index(string Searchstring)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            var categories = _categoryManager.GetAll();

            if (!string.IsNullOrEmpty(Searchstring))
            {
                categories = categories.Where(c => c.Code.Contains(Searchstring) || c.Name.Contains(Searchstring)).ToList();
            }
            categoryViewModel.Categories = categories;
            return View(categoryViewModel);
        }

        //***************Update Category*****************
        public ActionResult EditCategory(int id)
        {
            var category = _categoryManager.GetById(id);

            CategoryViewModel categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            categoryViewModel.Categories = _categoryManager.GetAll();
            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel categoryViewModel)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                Category category = Mapper.Map<Category>(categoryViewModel);

                categoryViewModel.Categories = _categoryManager.GetAll();
                //bool isExistCategory = _categoryManager.ExistCategory(category);
                //if (isExistCategory)
                //{
                //    ViewBag.existDuplicate = "Code is Already Exist..";
                //    return View(categoryViewModel);
                //}

                if (_categoryManager.Update(category))
                {
                    message = "Updated Successfully..";
                }
                else
                {
                    message = "No Change Your Update Information";
                }
            }
            else
            {
                ViewBag.InvalidModel = "ModelState is invalied!";
                ViewBag.existDuplicate = "";
                return View(categoryViewModel);
            }

            ViewBag.Message = message;
            return View(categoryViewModel);
        }

        //***************Show By Id*****************
        public ActionResult ShowByIdCategory(int id)
        {
            Category category = _categoryManager.GetById(id);

            //CategoryViewModel categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            //categoryViewModel.Categories = _categoryManager.GetAll();
            return View(category);
        }
    }
}