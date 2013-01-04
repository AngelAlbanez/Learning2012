using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcMovies.Models
{
    public class Movie
    {
        [ScaffoldColumn(false)]
        public int MovieId { get; set; }
        [DisplayName("Genre")]
        public int GenreId { get; set; }
        [DisplayName("Actor")]
        public int ActorId { get; set; }
        [Required(ErrorMessage="Price Required")]
        [Range(0.01, 100.00, ErrorMessage="Price must be between $0.01 and $100.00 (without $)")]
        public decimal Price { get; set; }
        [DisplayName("Movie Poster")]
        [StringLength(1024)]
        public string MovieArtUrl { get; set; }
        [Required(ErrorMessage="Field Required")]
        [StringLength(160)]
        public string Title { get; set; }

        //from public to public virtual
        public virtual Genre Genre { get; set; }
        public virtual Actor Actor { get; set; }
    }
}