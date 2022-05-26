
namespace soprosopro.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }

        public ICollection<MovieActors>? AM { get; set; }
        public ICollection<MovieDirectors>? DM { get; set; }
        public ICollection<MovieProducers>? PM { get; set; }
    }
}
