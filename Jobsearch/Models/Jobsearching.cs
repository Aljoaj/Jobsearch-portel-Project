using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobsearch.Models
{
    public class Jobsearching
    {
        public Jobsearching()
        {
            selectjob = new List<Jsearch>();
            insertse = new Jsearch();
        }
        public Jsearch insertse { set; get; }
        public List<Jsearch> selectjob { set; get; }
    }
    public class Jsearch
    {
        public int jobid { set; get; }
        public int comid { set; get; }
        public string jtitle { set; get; }
        public string jdesc { set; get; }
        public string jskills { set; get; }
        public string edate { set; get; }
        public string jstatus { set; get; }
        public string jmsg { set; get; }
    }
}