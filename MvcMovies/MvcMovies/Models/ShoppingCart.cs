using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovies.Models
{
    public partial class ShoppingCart
    {
        MovieStoreEntities storeDB = new MovieStoreEntities();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";


        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        //metodo para simplificar shopping cart calls

        //esta definicion expone los siguientes metodos:

        /*AddToCart
         * RemoveFromCart
         * EmptyCart
         * GetCartItems
         * GetCount
         * GetTotal
         * CreateOrder
         * GetCart*/

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Movie Movie)
        {

            //Obtener el carrito y las instancias de las peliculas
            var cartItem = storeDB.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId
                && c.MovieId == Movie.MovieId);

            if (cartItem == null)
            {
                //Crear un nuevo objecto de Cart si este no existe
                cartItem = new Cart
                {
                    MovieId = Movie.MovieId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                //si el objeto no existe en el carrito
                //agregar uno a la cuenta
                cartItem.Count++;
            }
            //Guardar los cambios
            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            //Obtener el carrito
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
                // Guardar Cambios
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
            //Guardar Cambios
            storeDB.SaveChanges();

        }

        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            //Obtener la cuenta de cada objeto y sumarla
            int? count = (from CartItems in storeDB.Carts
                          where CartItems.CartId == ShoppingCartId
                          select (int?)CartItems.Count).Sum();

            //Regresa 0 si todas las entradas on nulas
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            /* multiplica el valor del objeto por le numero de objectos y 
             * los sumas con los demas objetos en si, para
             * objeter el total de la venta */

            decimal? total = (from CartItems in storeDB.Carts
                              where CartItems.CartId == ShoppingCartId
                              select (int?)CartItems.Count *
                              CartItems.Movie.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order Order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            //loop sobre lso items del carrito,

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    MovieId = item.MovieId,
                    OrderId = Order.OrderId,
                    UnitPrice = item.Movie.Price,
                    Quantity = item.Count
                };
                orderTotal += (item.Count * item.Movie.Price);

                storeDB.OrderDetails.Add(orderDetail);


               

            }

            Order.Total = orderTotal;

            //Guardar la orden

            storeDB.SaveChanges();

            //Vaciar el carro de compra

            EmptyCart();

            //Regresar el OrderID como numero de confirmacion

            return Order.OrderId;

            
        }

        //PENDIENTE

        //Permitir acceso a las cookies
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
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        //Una vez que el usuario ha accedido a la aplicacion
        //Se le asigna un carrito de compra a su nombre de usuario

        public void MigrateCart(string UserName)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = UserName;
            }
            storeDB.SaveChanges();

        }



    }
}