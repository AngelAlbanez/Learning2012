using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovies.Models;

namespace MvcMovies.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var movie = new Movie()
                {
                    Title = "Movie " + id
                };

            
            return View(movie);
        }

    }
}
