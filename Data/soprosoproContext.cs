using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using soprosopro.Models;

namespace soprosopro.Models
{
    public class soprosoproContext : DbContext
    {
        public soprosoproContext (DbContextOptions<soprosoproContext> options)
            : base(options)
        {
        }

        public DbSet<soprosopro.Models.Movie>? Movie { get; set; }

        public DbSet<soprosopro.Models.Genre>? Genre { get; set; }

        public DbSet<soprosopro.Models.Person>? Person { get; set; }
        public DbSet<soprosopro.Models.MovieGenres>? MovieGenres { get; set; }
        public DbSet<soprosopro.Models.MovieActors>? MovieActors { get; set; }
        public DbSet<soprosopro.Models.MovieDirectors>? MovieDirectors { get; set; }
        public DbSet<soprosopro.Models.MovieProducers>? MovieProducers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieGenres>()
            .HasOne<Movie>(p => p.Movie)
            .WithMany(p => p.Genres)
            .HasForeignKey(p => p.MovieId);

            builder.Entity<MovieGenres>()
         .HasOne<Genre>(p => p.Genres)
         .WithMany(p => p.Movies)
         .HasForeignKey(p => p.GenreId);

            builder.Entity<MovieActors>()
            .HasOne<Movie>(p => p.Movie)
            .WithMany(p => p.Actors)
            .HasForeignKey(p => p.MovieId);
            //.HasPrincipalKey(p => p.Id);

            builder.Entity<MovieProducers>()
                 .HasOne<Movie>(p => p.Movie)
                 .WithMany(p => p.Producers)
                 .HasForeignKey(p => p.MovieId);

            builder.Entity<MovieDirectors>()
          .HasOne<Movie>(p => p.Movie)
          .WithMany(p => p.Directors)
          .HasForeignKey(p => p.MovieId);
            //.HasPrincipalKey(p => p.Id);

            builder.Entity<MovieActors>()
           .HasOne<Person>(p => p.Actor)
           .WithMany(p => p.AM)
           .HasForeignKey(p => p.ActorId);
            //.HasPrincipalKey(p => p.Id);

            builder.Entity<MovieProducers>()
                 .HasOne<Person>(p => p.Producer)
                 .WithMany(p => p.PM)
                 .HasForeignKey(p => p.ProducerId);

            builder.Entity<MovieDirectors>()
          .HasOne<Person>(p => p.Director)
          .WithMany(p => p.DM)
          .HasForeignKey(p => p.DirectorId);
            //.HasPrincipalKey(p => p.Id);

        }
    }
}
