using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity

namespace MvcMovie.Models
{
    public class MovieEntity : MovieDbContext
    {

        public DbSet<Movies> Movieses { get; set; }
        
    }
}