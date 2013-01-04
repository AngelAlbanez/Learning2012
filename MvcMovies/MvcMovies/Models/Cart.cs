using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovies.Models
{
    public class Cart
    {
        
        public int RecordId { get; set; }
        public string CartId { get; set; } //allow anonymous shopping
        public int MovieId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Movie Movie { get; set; }
    }
}