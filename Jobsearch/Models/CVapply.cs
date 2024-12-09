using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jobsearch.Models
{
    public class CVapply
    {
        public int uid { set; get; }
        public int jid { set; get; }
        public string resum { set; get; }
        public DateTime adate { set; get; }
        public string appmsg { set; get; }
    }
}