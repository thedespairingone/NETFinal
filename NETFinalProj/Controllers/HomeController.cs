using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETFinalProj.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult SignIn(string name, string password)
        {
            var judge = "-1";
            if (name == "user" && password == "1234")
            {
                judge = "1";
            }

            return Json(judge, JsonRequestBehavior.AllowGet);
        }
    }
}