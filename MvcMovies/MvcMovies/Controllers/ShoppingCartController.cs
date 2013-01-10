using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MvcMovies.Models;
using MvcMovies.ViewModels;
//using System.Model.MovieStoreEntities;

namespace MvcMovies.Controllers
{
    public class ShoppingCartController : Controller
    {
        MvcMovies.Models.MovieStoreEntities storeDB = new MvcMovies.Models.MovieStoreEntities();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = MvcMovies.Models.ShoppingCart.GetCart(this.HttpContext);

            // Configuracion del viewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Regresar la vista
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Regresar las peliculas de la base de datos
            var addedMovie = storeDB.Movies
                .Single(movie => movie.MovieId == id);

            // Agregarlos al carrito de compra
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedMovie);

            // Regresar a la tienda por mas compras
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Quitar objeto del carrito
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Objetener nombre de la pelicula
            string movieName = storeDB.Carts
                .Single(item => item.RecordId == id).Movie.Title;

            // Quietar del carrito
            int itemCount = cart.RemoveFromCart(id);

            // Mostrar mensaje de confirmacion
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(movieName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }


    }
}
