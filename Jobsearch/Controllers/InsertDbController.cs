using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jobsearch.Models;

namespace Jobsearch.Controllers
{
    public class InsertDbController : Controller
    {
        ProjectmvcEntities dbobj = new ProjectmvcEntities();
        // GET: InsertDb
        public ActionResult Userpageload()
        {
            UserInsert user = new UserInsert();
            user.MyFavoriteQual = getQualificationData();
            return View(user);
        }
        public List<CheckBoxListHelper> getQualificationData()
        {
            List<CheckBoxListHelper> sts = new List<CheckBoxListHelper>()
            {
                new CheckBoxListHelper {Value="SSLC",Text="SSLC",IsChecked=true},
                new CheckBoxListHelper {Value="PLUS TWO",Text="PLUS TWO",IsChecked=false},
                new CheckBoxListHelper {Value="BCA",Text="BCA",IsChecked=false},
                new CheckBoxListHelper {Value="MCA",Text="MCA",IsChecked=false},
                new CheckBoxListHelper {Value="BTECH",Text="BTECH",IsChecked=false},
            };
            return sts;
        }
        public ActionResult usInsert_click(UserInsert clsobj, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/Photos");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);

                    var fullpath = Path.Combine("~\\Photos", fname);
                    clsobj.photo = fullpath;
                }

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

                var quid = string.Join(",",clsobj.selectedQual);
                clsobj.qual = quid;
                clsobj.MyFavoriteQual = getQualificationData();

                dbobj.sp_userReg(regid, clsobj.name, clsobj.age, clsobj.address, clsobj.phone, clsobj.email, clsobj.photo, clsobj.skills, clsobj.expinyear, clsobj.location, clsobj.qual);
                dbobj.sp_Loginsert(regid, clsobj.username, clsobj.pass, "User");
                clsobj.usermsg = "Successfully Inserted";
                return View("Userpageload", clsobj);
            }
            else
            {
                clsobj.MyFavoriteQual = getQualificationData();
            }
            return View("Userpageload", clsobj);
        }
    }
}