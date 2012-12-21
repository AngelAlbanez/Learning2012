using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovies.Models;

namespace MvcMovies.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        MovieStoreEntities storeDB=new MovieStoreEntities();

        public ActionResult Index()
        {
            //var genres = new List<Genre>
            //    {
            //        new Genre {Name = "Suspense"},
            //        new Genre {Name = "Action"},
            //        new Genre {Name = "Thriller"},
            //        new Genre {Name = "Kids"},
            //        new Genre {Name = "Porn"},
            //        new Genre {Name = "Documental"},
            //        new Genre {Name = "5 Stars"},
            //        new Genre {Name = "Tutorial"},
            //        new Genre {Name = "Comedy"},
            //        new Genre {Name = "Drama"}
            //    };

            //llamada a la base de datos, en lugar de llamar a los datos ficticios antes puestos
            var genres = storeDB.Genres.ToList();
            return View(genres);
        }

        //public string Browse()
        //{
        //    return "Hello from Store.Browse()";
        //}

        public ActionResult Details(int id)
        {
            var movie = storeDB.Movies.Find(id);
            return View(movie);
        }

        public ActionResult Browse(string genre)
        {
            var genreModel = storeDB.Genres.Include("Movies").Single(g => g.Name == genre);
            return View(genreModel);
        }
    }
}
