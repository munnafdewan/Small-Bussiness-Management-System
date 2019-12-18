using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Error404.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Error404.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Code Field is required..")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Code Must be 4 digit length..")]
        public string Code { get; set; }
        [Required(ErrorMessage = "The Name Field is required..")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ReorderLevel is required..")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public double ReorderLevel { get; set; }
        [Required(ErrorMessage = "Description Field is required..")]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }
    }
}