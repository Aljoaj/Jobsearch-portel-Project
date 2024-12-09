using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jobsearch.Models
{
    public class comInsert
    {
        [Required(ErrorMessage = "Enter the name")]
        public string name { set; get; }
        [Required(ErrorMessage = "Enter the address")]
        public string address { set; get; }
        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string email { set; get; }
        [Required(ErrorMessage = "Enter the Phone")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid number")]
        public string phone { set; get; }
        [Required(ErrorMessage = "Enter website name")]
        public string website { set; get; }
        public string username { set; get; }
        public string pass { set; get; }
        [Compare("pass", ErrorMessage = "Password mismatch")]
        public string cpassword { set; get; }
        public string adminmsg { set; get; }
    }
}