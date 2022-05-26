using Microsoft.EntityFrameworkCore;
using soprosopro.Models;
#nullable disable
namespace soprosopro.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new soprosoproContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<soprosoproContext>>());
            // Look for any movies.
            if (context.Movie.Any() || context.Person.Any() || context.Genre.Any())
            {
                return; // DB has been seeded
            }
            context.Movie.AddRange(
                   new Movie
                   {
                       Title = "abc"
                   },
                     new Movie
                     {
                         Title = "cba"
                     }
                   ); context.SaveChanges();

            context.Genre.AddRange(
                new Genre
                {
                    Type = "Comedy"
                },
                 new Genre
                 {
                     Type = "Slice Of Life"
                 }
                );
            context.SaveChanges();
            context.Person.AddRange(
                new Person
                {
                    Name = "Mike"
                },
                 new Person
                 {
                     Name = "Pyke"
                 }
                );
            context.SaveChanges();

            context.MovieGenres.AddRange(
                new MovieGenres
                {
                    MovieId=1,
                    GenreId=1
                },
                 new MovieGenres
                 {
                     MovieId = 2,
                     GenreId = 1
                 },
                  new MovieGenres
                  {
                      MovieId = 1,
                      GenreId = 2
                  }
                );

            context.SaveChanges();

            context.MovieActors.AddRange(
               new MovieActors
               {
                   MovieId = 1,
                   ActorId = 1
               },
                new MovieActors
                {
                    MovieId = 2,
                    ActorId = 1
                },
                 new MovieActors
                 {
                     MovieId = 1,
                     ActorId = 2
                 }
               );

            context.SaveChanges();
            context.MovieDirectors.AddRange(
                new MovieDirectors
                {
                    MovieId = 1,
                    DirectorId = 1
                },
                 new MovieDirectors
                 {
                     MovieId = 2,
                     DirectorId = 1
                 },
                  new MovieDirectors
                  {
                      MovieId = 1,
                      DirectorId = 2
                  }
                );

            context.SaveChanges(); 
            context.MovieProducers.AddRange(
                new MovieProducers
                {
                    MovieId = 1,
                    ProducerId = 1
                },
                 new MovieProducers
                 {
                     MovieId = 2,
                     ProducerId = 1
                 },
                  new MovieProducers
                  {
                      MovieId = 1,
                      ProducerId = 2
                  }
                );

            context.SaveChanges();
        }
    }
}