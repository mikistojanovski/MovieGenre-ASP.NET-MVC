using System.ComponentModel.DataAnnotations.Schema;

namespace soprosopro.Models
{
    public class Genre
    {
     
        public int GenreId { get; set; }
        public string Type { get; set; }
        public ICollection<MovieGenres>? Movies { get; set; }
    }
}
