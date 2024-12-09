using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Jobsearch.Models;
using System.Configuration;

namespace Jobsearch.Controllers
{
    public class ApplyCVController : Controller
    {
        ProjectmvcEntities dbobj = new ProjectmvcEntities();
        // GET: ApplyCV
        public ActionResult ApplyCV_Load(int cid,int jid)
        {
            TempData["cid"] = cid;
            TempData["jid"] = jid;
            return View();
        }
        [HttpPost]
        public ActionResult ApplyClick(HttpPostedFileBase file)
        {
            if(ModelState.IsValid)
            {

                if(file!=null && file.ContentLength>0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var sp = Server.MapPath("~/Resums");
                    string pa = Path.Combine(sp, fname);
                    file.SaveAs(pa);

                    var fullpath = Path.Combine("~/Resums", fname);

                    DateTime adate = DateTime.Now;
                    
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cvpage"].ConnectionString))

                    {
                        SqlCommand cmd = new SqlCommand("sp_resumeApply", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["uid"]));
                        cmd.Parameters.AddWithValue("@joid", Convert.ToInt32(Session["jid"]));
                        cmd.Parameters.AddWithValue("@resm", fullpath);
                        cmd.Parameters.AddWithValue("@sts", "Available");
                        cmd.Parameters.AddWithValue("@dat", adate);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    var cvobj = new CVapply
                    {
                        appmsg = "Your Application is Submitted"
                    };
                    return View("ApplyCV_Load", cvobj);
                }
                return View("ApplyCV_Load");
            }
            return View("ApplyCV_Load");
        }
    }
}