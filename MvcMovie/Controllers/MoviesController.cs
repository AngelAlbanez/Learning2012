using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{ 
    public class MoviesController : Controller
    {
        private MovieDbContext db = new MovieDbContext();

        //
        // GET: /Movies/

        public ViewResult Index()
        {
            return View(db.Movies.ToList());
        }

        //
        // GET: /Movies/Details/5

        public ViewResult Details(int id)
        {
            Movies movies = db.Movies.Find(id);
            return View(movies);
        }

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Movies/Create

        [HttpPost]
        public ActionResult Create(Movies movies)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movies);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(movies);
        }
        
        //
        // GET: /Movies/Edit/5
 
        public ActionResult Edit(int id=0)
        {
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult Edit(Movies movies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movies);
        }

        //
        // GET: /Movies/Delete/5
 
        public ActionResult Delete(int id)
        {
            Movies movies = db.Movies.Find(id);
            return View(movies);
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Movies movies = db.Movies.Find(id);
            db.Movies.Remove(movies);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SearchIndex(string searchString)
        {
            
            var movies = from m in db.Movies
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(movies);
        }




    }
}