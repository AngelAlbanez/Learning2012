using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovies.Models
{
    public class Genre
    {
        public string Name { get; set; }
        public int GenreId { get; set; }
        public string Description { get; set; }
        public List<Movie> Movies { get; set; } 
    }
}