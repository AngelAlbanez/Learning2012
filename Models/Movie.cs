using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovies.Models
{
    public class Movie
    {

        public int MovieId { get; set; }
        public int GenreId { get; set; }
        public int ActorId { get; set; }
        public decimal Price { get; set; }
        public string MovieArtUrl { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public Actor Actor { get; set; }
    }
}