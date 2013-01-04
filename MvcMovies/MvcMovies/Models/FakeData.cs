using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcMovies.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<MovieStoreEntities>
    {
        protected override void Seed(MovieStoreEntities context)
        {
            var genres = new List<Genre>
            {
                new Genre {Name = "Suspense"},
				new Genre {Name = "Action"},
				new Genre {Name = "Thriller"},
				new Genre {Name = "Kids"},
				new Genre {Name = "Fiction"},
				new Genre {Name = "Documental"},
				new Genre {Name = "5 Stars"},
				new Genre {Name = "Tutorial"},
				new Genre {Name = "Comedy"},
				new Genre {Name = "Drama"}
            };

            var Actors = new List<Actor>
            {
                new Actor { Name = "Jack Nicholson" },
                new Actor { Name = "Al Pachino" },
                new Actor { Name = "Leonardo Di Caprio" },
                new Actor { Name = "Helena Boham Carter" },
                new Actor { Name = "Natalie Portman" },
                new Actor { Name = "Uma Truman" },
                new Actor { Name = "Bela Lugosi" },
                new Actor { Name = "Milla Jovovich" },
                new Actor { Name = "Drew Barrymore" },
                new Actor { Name = "Monica Bellucci" },
                new Actor { Name = "Neve Campbell" },
                new Actor { Name = "Jodie Foster" },
                new Actor { Name = "Carrie Ann Moss" },
                new Actor { Name = "Bridget Fonda" },
                new Actor { Name = "Jessica Alba" },

            };

            new List<Movie>
            {
                new Movie {
                    Title = "El silencio de los inocentes", 
                    Genre = genres.Single(g => g.Name == "Suspense"), 
                    Price = 8.99M, 
                    Actor = Actors.Single(a => a.Name == "Jodie Foster"), 
                    MovieArtUrl = "/Content/Images/placeholder.gif" 
                },
                new Movie { Title = "Kill Bill Vol. 2", Genre = genres.Single(g => g.Name == "Action"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Uma Truman"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "Inception", Genre = genres.Single(g => g.Name == "Drama"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Leonardo Di Caprio"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "Resident Evil Resurrection", Genre = genres.Single(g => g.Name == "Fiction"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Milla Jovovich"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "Black Swan", Genre = genres.Single(g => g.Name == "Drama"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Natalie Portman"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "Passion of Christ", Genre = genres.Single(g => g.Name == "Documental"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Monica Bellucci"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "The Matrix Revolution", Genre = genres.Single(g => g.Name == "Fiction"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Carrie Ann Moss"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "The Illusionist", Genre = genres.Single(g => g.Name == "Fiction"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Jessica Alba"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "The Fem Nikita", Genre = genres.Single(g => g.Name == "Action"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Bridget Fonda"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "The nightmare before christmas", Genre = genres.Single(g => g.Name == "Kids"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Helena Boham Carter"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "Shoot them up", Genre = genres.Single(g => g.Name == "Action"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Monica Bellucci"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "Constantine", Genre = genres.Single(g => g.Name == "Fiction"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Neve Campbell"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "The Terror Room", Genre = genres.Single(g => g.Name == "Drama"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Jodie Foster"), MovieArtUrl = "/Content/Images/placeholder.gif" },
                new Movie { Title = "No Country for Old Man", Genre = genres.Single(g => g.Name == "Drama"), Price = 8.99M, Actor = Actors.Single(a => a.Name == "Neve Campbell"), MovieArtUrl = "/Content/Images/placeholder.gif" },
               
            }.ForEach(a => context.Movies.Add(a));
        }
    }
}