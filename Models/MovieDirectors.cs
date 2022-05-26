using System.ComponentModel.DataAnnotations;

namespace soprosopro.Models
{
    public class MovieDirectors
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int DirectorId { get; set; }
        public Person? Director { get; set; }
    }
}
