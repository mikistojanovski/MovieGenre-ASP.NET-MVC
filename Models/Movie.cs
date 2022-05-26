using System.ComponentModel.DataAnnotations;
namespace soprosopro.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }


        public ICollection<MovieActors>? Actors { get; set; }
        public ICollection<MovieDirectors>? Directors { get; set; }
        public ICollection<MovieProducers>? Producers { get; set; }
        public ICollection<MovieGenres>? Genres { get; set; }


    }
}
