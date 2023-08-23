using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace TestSiteFrameworkWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {           
            ViewBag.Title = "Home Page";            
            Static.Path = ViewBag.Path = Server.MapPath("~/bin/BBox");
            //ViewBag.Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return View();
        }
    }
}
