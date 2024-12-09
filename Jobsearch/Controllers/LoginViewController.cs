using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jobsearch.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Jobsearch.Controllers
{
    public class LoginViewController : Controller
    {
        ProjectmvcEntities dbobj = new ProjectmvcEntities();
        // GET: LoginView
        public ActionResult LoginPageload()
        {
            return View();
        }
        public ActionResult UserHomePage()
        {
            return View(getData());
        }
        public ActionResult ComHomePage()
        {
            return View();
        }
        public ActionResult Login_Click(LoginCls objcls)
        {
            if (ModelState.IsValid)
            {
                var val = dbobj.sp_LoginCountId(objcls.Uname, objcls.password).Single();
                if (val == 1)
                {
                    var uid = dbobj.sp_LoginId(objcls.Uname, objcls.password).FirstOrDefault();
                    Session["uid"] = uid;

                    var lt = dbobj.sp_LoginType(objcls.Uname, objcls.password).FirstOrDefault();
                    if (lt == "User")
                    {
                        return RedirectToAction("UserHomePage");
                    }
                    else if (lt == "Company")
                    {
                        return RedirectToAction("ComHomePage");
                    }
                }
            }
            else
            {
                ModelState.Clear();
                objcls.msg = "Invalid login";
                return View("LoginPageload", objcls);
            }
            return View("LoginPageload", objcls);
        }
        public ActionResult JobinsClick(Jobinsert clsobj)
        {
            var comid = Session["uid"];
            var uid = Convert.ToInt32(comid);
            DateTime endtime = clsobj.Enddate;
            dbobj.sp_Jobinsert(uid, clsobj.JobTitle, clsobj.JobDes, clsobj.JobSkills, endtime, "Open");
            clsobj.Cmsg = "Successfully Updated";
            return View("ComHomePage",clsobj);
        }
        private Jobsearching getData()
        {
            var joblist = new Jobsearching();
            List<string>lst = new List<string>();
            var job = dbobj.JobTabs.ToList();
            foreach (var e in job)
            {
                var jobcls = new Jsearch();
                {
                    jobcls.jobid = e.Jobid;
                    jobcls.comid = e.Comid;
                    jobcls.jtitle = e.JobTittle;
                    jobcls.jdesc = e.JobDesc;
                    jobcls.jskills = e.JobSkills;
                    jobcls.edate = e.EndDate.ToString("yyyy-MM-dd");
                    jobcls.jstatus = e.Status;

                    Session["jid"] = jobcls.jobid;
                };
                joblist.selectjob.Add(jobcls);
                var s = jobcls.jskills;
                lst.Add(s);

                
            }
            TempData["ski"] = string.Join(" ", lst);
            return joblist;
        }
        public ActionResult searchjob_Click(Jobsearching aobj)
        {
            string qry = "";
            if(!string.IsNullOrWhiteSpace(aobj.insertse.jtitle))
            {
                qry += " and JobTittle like '%" + aobj.insertse.jtitle + "%'";
            }
            if (!string.IsNullOrWhiteSpace(aobj.insertse.jdesc))
            {
                qry += " and JobDesc like '%" + aobj.insertse.jdesc + "%'";
            }
            if (!string.IsNullOrWhiteSpace(aobj.insertse.jskills))
            {
                qry += " and JobSkills like '%" + aobj.insertse.jskills + "%'";
            }
            return View("UserHomePage", getdata1(aobj, qry));
        }
        private Jobsearching getdata1(Jobsearching aobj,string qry)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Jobsearching", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qry",qry);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var joblist = new Jobsearching();
                while(dr.Read())
                {
                    var jobclss = new Jsearch();
                    jobclss.comid = Convert.ToInt32(dr["Comid"].ToString());
                    jobclss.jtitle = dr["JobTittle"].ToString();
                    jobclss.jdesc = dr["JobDesc"].ToString();
                    jobclss.jskills = dr["JobSkills"].ToString();
                    jobclss.edate = dr["EndDate"].ToString();
                    jobclss.jstatus = dr["Status"].ToString();

                    joblist.selectjob.Add(jobclss);
                }
                con.Close();
                return joblist;
            }
        }

    }
}