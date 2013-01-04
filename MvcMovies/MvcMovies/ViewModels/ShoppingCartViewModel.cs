using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovies.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<MvcMovies.Models.Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}