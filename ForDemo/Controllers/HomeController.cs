using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForDemo.Controllers
{
    public class HomeController : Controller
    {
        [MyAuthorize]
        public ActionResult Index()
        {
            return View();
        } 
    }
}