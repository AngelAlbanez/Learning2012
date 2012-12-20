using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Models
{
    public partial class ShoppingCart
    {
        MovieEntity storeDB = new MovieEntity();;
        string ShopingCartId { get; set; }
        public const string CartSession ="CartId"
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShopingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Movies movies)
        {
            
            var cartItem = storeDB.carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId 
                && c.CartId == movies.id);
 
            if (cartItem == null)
            {
                
                cartItem = new Cart
                {
                    movieId = movies.id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
               
                cartItem.Count++;
            }
            
            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
           
            var cartItem = storeDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId 
                && cart.RecordId == id);
 
            int itemCount = 0;
 
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
                
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);
 
            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
           
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
           
            return count ?? 0;
        }
        public decimal GetTotal()
        {
           
            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.movies.Price).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
 
            var cartItems = GetCartItems();
       
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    MovieId = item.id,
                    OrderId = order.OrderId,
                    UnitPrice = item.movies.Price,
                    Quantity = item.Count
                };
           
                orderTotal += (item.Count * item.Album.Price);
 
                storeDB.OrderDetails.Add(orderDetail);
 
            }
          
            order.Total = orderTotal;
 
   
            storeDB.SaveChanges();
        
            EmptyCart();
           
            return order.OrderId;
        }
        
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    
                    Guid tempCartId = Guid.NewGuid();
               
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);
 
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}

    }
}