using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jobsearch.Models;

namespace Jobsearch.Controllers
{
    public class ComregController : Controller
    {
        ProjectmvcEntities dbobj = new ProjectmvcEntities();
        // GET: Comreg
        public ActionResult ComPageload()
        {
            return View();
        }
        public ActionResult InsertCom_click(comInsert clsobj)
        {
            if (ModelState.IsValid)
            {
                var getmaxid = dbobj.sp_MaxIdLogin().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }
                dbobj.sp_comRegTab(regid, clsobj.name, clsobj.address, clsobj.email, clsobj.phone,clsobj.website);
                dbobj.sp_Loginsert(regid, clsobj.username, clsobj.pass, "Company");
                clsobj.adminmsg = "Successfully Inserted";
                return View("ComPageload", clsobj);
            }
            return View("ComPageload", clsobj);
        }
    }
}