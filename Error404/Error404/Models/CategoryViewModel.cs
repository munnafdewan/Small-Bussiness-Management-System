using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Error404.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Error404.Models
{
    public class CategoryViewModel
    {

        public CategoryViewModel()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Code must be 4 digit length..")]
        [Display(Name = "Category Code: ")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Field must be required..")]
        [Display(Name = "Category Name: ")]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}