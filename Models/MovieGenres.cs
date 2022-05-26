using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace soprosopro.Models
{
    public class MovieGenres
    {

        public int Id { get; set; }
      
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
       
        public int GenreId { get; set; }
        public Genre? Genres { get; set; }
    }
}
