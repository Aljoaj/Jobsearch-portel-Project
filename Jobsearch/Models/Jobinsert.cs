using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jobsearch.Models
{
    public class Jobinsert
    {
        [Required(ErrorMessage ="Enter Job Title")]
        public string JobTitle { set; get; }
        [Required(ErrorMessage ="Enter The description")]
        public string JobDes { set; get; }
        [Required(ErrorMessage ="Enter The Valid Jobskills")]
        public string JobSkills { set; get; }
        public DateTime Enddate { set; get; }
        public string Cmsg { set; get; }
    }
}