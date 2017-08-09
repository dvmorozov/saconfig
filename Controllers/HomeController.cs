using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAConfig.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to SCL it - on-line SCL editing service!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
