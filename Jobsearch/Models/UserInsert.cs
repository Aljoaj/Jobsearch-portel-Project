using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jobsearch.Models
{
    public class CheckBoxListHelper
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class UserInsert
    {
        public List<CheckBoxListHelper> MyFavoriteQual { get; set; }
        public string[] selectedQual { get; set; }
        public int uid { set; get; }
        [Required(ErrorMessage = "Enter the name")]
        public string name { set; get; }
        [Range(18, 60, ErrorMessage = "Enter the age")]
        public int age { set; get; }
        [Required(ErrorMessage = "Enter the address")]
        public string address { set; get; }
        [Required(ErrorMessage = "Enter the Phone")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid number")]
        public string phone { set; get; }
        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string email { set; get; }
        public string photo { set; get; }
        [Required(ErrorMessage = "Enter your skills")]
        public string skills { set; get; }
        [Required(ErrorMessage = "Enter your experience")]
        public string expinyear { set; get; }
        [Required(ErrorMessage = "Enter your location")]
        public string location { set; get; }
        public string qual { set; get; }
        public string username { set; get; }
        public string pass { set; get; }
        [Compare("pass", ErrorMessage = "Password mismatch")]
        public string cpassword { set; get; }
        public string usermsg { set; get; }
    }
}