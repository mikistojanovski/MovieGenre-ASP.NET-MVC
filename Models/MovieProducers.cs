using System.ComponentModel.DataAnnotations;

namespace soprosopro.Models
{
    public class MovieProducers
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int ProducerId { get; set; }
        public Person? Producer { get; set; }
    }
}
