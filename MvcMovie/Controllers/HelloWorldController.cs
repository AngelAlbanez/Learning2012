using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: /HelloWorld/

        public ViewResult Index()
        {
            return View();
        }


        public ActionResult Welcome(string name, int numTimes=1)
        {
            ViewBag.message = "Name: " + name;
            ViewBag.numTimes = numTimes;

            return View();
        }


    }
}
