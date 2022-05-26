using Microsoft.AspNetCore.Mvc.Rendering;
using soprosopro.Models;

namespace soprosopro.ViewModels
{
    public class SelectGenres
    {
        public Movie Movie { get; set; }
        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        public IEnumerable<int>? SelectedActors { get; set; }
        public IEnumerable<SelectListItem>? ActorList { get; set; }
        public IEnumerable<int>? SelectedProducers { get; set; }
        public IEnumerable<SelectListItem>? ProducerList { get; set; }
        public IEnumerable<int>? SelectedDirectors { get; set; }
        public IEnumerable<SelectListItem>? DirectorList { get; set; }

    }
}
