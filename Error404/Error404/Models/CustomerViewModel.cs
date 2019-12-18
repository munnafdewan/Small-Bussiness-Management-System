using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Error404.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Error404.Models
{
    public class CustomerViewModel
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Can not be Empty")]
        [MaxLength(4, ErrorMessage = "Maximum Lenght is 4")]
        [MinLength(4, ErrorMessage = "Minimum Lenght is 4")]
        [Display(Name = "Code:")]
        public string Code { set; get; }
        [Required(ErrorMessage = "Can not be Empty")]
        [Display(Name = "Name:")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Can not be Empty")]
        [Display(Name = "Address:")]
        public string Address { set; get; }
        [Required(ErrorMessage = "Can not be Empty")]
        [Display(Name = "Email:")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Can not be Empty")]
        [MaxLength(11, ErrorMessage = "Maximum Lenght is 11")]
        [Display(Name = "Contact:")]
        public string Contact { set; get; }
        [Required(ErrorMessage = "Can not be Empty")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Loyality:")]
        public int Loyality { set; get; }
        public List<Customer> Customers { set; get; }
    }
}