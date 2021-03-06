﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovies.Models;

namespace MvcMovies.Controllers
{ 
    [Authorize(Roles="Administrator")]
    public class StoreManagerController : Controller
    {
        private MovieStoreEntities db = new MovieStoreEntities();

        //
        // GET: /StoreManager/

        public ViewResult Index()
        {
            var movies = db.Movies
                .Include(m => m.Genre)
                .Include(m => m.Actor);
            return View(movies.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ViewResult Details(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "Name");
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", movie.GenreId);
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "Name", movie.ActorId);
            return View(movie);
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Movie movie = db.Movies.Find(id);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", movie.GenreId);
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "Name", movie.ActorId);
            return View(movie);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", movie.GenreId);
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "Name", movie.ActorId);
            return View(movie);
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}