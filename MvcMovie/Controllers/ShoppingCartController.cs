using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;
using MvcMovie.Viewmodel;

namespace MvcMovie.Controllers
{
    public class ShoppingCartController : Controller
    {
      
         MovieEntity storeDB = new MovieEntity();
     
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
 
        
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
         
            return View(viewModel);
        }
    
        public ActionResult AddToCart(int id)
        {
            
            var addedAlbum = storeDB.Movies
                .Single(movies => movies.id == id);
 
            
            var cart = ShoppingCart.GetCart(this.HttpContext);
 
            cart.AddToCart(addedAlbum);
 
           
            return RedirectToAction("Index");
        }
       
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
           
            var cart = ShoppingCart.GetCart(this.HttpContext);
 
           
            string MovieName = storeDB.Carts
                .Single(item => item.id == id).moview.Title;
 
           
            int itemCount = cart.RemoveFromCart(id);
 
            
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(MovieName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
 
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
    }
}
