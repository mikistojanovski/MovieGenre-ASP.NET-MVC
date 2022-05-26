using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using soprosopro.Models;
using soprosopro.ViewModels;


namespace soprosopro.Controllers
{
    public class MoviesController : Controller
    {
        private readonly soprosoproContext _context;

        public MoviesController(soprosoproContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var index = _context.Movie
            .Include(m => m.Genres).ThenInclude(m => m.Genres)
            .Include(m => m.Actors).ThenInclude(m => m.Actor)
            .Include(m => m.Directors).ThenInclude(m => m.Director)
            .Include(m => m.Producers).ThenInclude(m => m.Producer)
            .AsNoTracking();
            await _context.SaveChangesAsync();
            return View(index);
            return _context.Movie != null ?
                          View(await _context.Movie.ToListAsync()) :
                          Problem("Entity set 'SoproTestContext.Movie'  is null.");

        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Genres).ThenInclude(m => m.Genres)
                .Include(m => m.Actors).ThenInclude(m => m.Actor)
                .Include(m => m.Directors).ThenInclude(m => m.Director)
                .Include(m => m.Producers).ThenInclude(m => m.Producer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MovieId == id);

            await _context.SaveChangesAsync();
            if (movie == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return View(movie);
        }
        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await _context.SaveChangesAsync();
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }


            var movie = _context.Movie.Where(m => m.MovieId == id)
               .Include(m => m.Genres)
               .Include(m => m.Actors)
               .Include(m => m.Producers)
               .Include(m => m.Directors)
               .First();

            
            if (movie == null)
            {
                return NotFound();
            }


            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(s => s.Type);

            var actors = _context.Person.AsEnumerable();
            actors = actors.OrderBy(s => s.Name);

            var directors = _context.Person.AsEnumerable();
            directors = directors.OrderBy(s => s.Name);

            var producers = _context.Person.AsEnumerable();
            producers = producers.OrderBy(s => s.Name);

            await _context.SaveChangesAsync();
            SelectGenres viewmodel = new SelectGenres
            {
                Movie = movie,
                GenreList = new MultiSelectList(genres, "GenreId", "Type"),
                SelectedGenres = movie.Genres.Select(s => s.GenreId),
                ActorList = new MultiSelectList(actors, "PersonId", "Name"),
                SelectedActors = movie.Actors.Select(s => s.ActorId),
                ProducerList = new MultiSelectList(producers, "PersonId", "Name"),
                SelectedProducers = movie.Producers.Select(s => s.ProducerId),
                DirectorList = new MultiSelectList(directors, "PersonId", "Name"),
                SelectedDirectors = movie.Directors.Select(s => s.DirectorId)
            };
            await _context.SaveChangesAsync();
            return View(viewmodel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SelectGenres viewmodel)
        {
            if (id != viewmodel.Movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Movie);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> listG = viewmodel.SelectedGenres;
                    IEnumerable<int> listActors = viewmodel.SelectedActors;
                    IEnumerable<int> ld = viewmodel.SelectedDirectors;
                    IEnumerable<int> pd = viewmodel.SelectedProducers;
                    //GENRES
                    IQueryable<MovieGenres> toBeRemoved = _context.MovieGenres.Where(s => !listG.Contains(s.GenreId) && s.MovieId == id);
                    _context.MovieGenres.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> existg = _context.MovieGenres.Where(s => listG.Contains(s.GenreId) && s.MovieId == id).Select(s => s.GenreId);
                    IEnumerable<int> newg = listG.Where(s => !existg.Contains(s));
                    await _context.SaveChangesAsync();
                    foreach (int gi in newg)
                         _context.MovieGenres.Add(new MovieGenres { MovieId = id, GenreId = gi });
                    
                    await _context.SaveChangesAsync();
                    //ACTOS

                    IQueryable<MovieActors> BeRemoved = _context.MovieActors.Where(s => !listActors.Contains(s.ActorId) && s.MovieId == id);
                    _context.MovieActors.RemoveRange(BeRemoved);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> existActors = _context.MovieActors.Where(s => listActors.Contains(s.ActorId) && s.MovieId == id).Select(s => s.ActorId);
                    IEnumerable<int> newActors = listActors.Where(s => !existActors.Contains(s));
                    foreach (int actorId in newActors)
                        _context.MovieActors.Add(new MovieActors { ActorId = actorId, MovieId = id });
                    await _context.SaveChangesAsync();

                    //DIRECTORS
                    IQueryable<MovieDirectors> Removed = _context.MovieDirectors.Where(s => !ld.Contains(s.DirectorId) && s.MovieId == id);
                    _context.MovieDirectors.RemoveRange(Removed);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> ed = _context.MovieDirectors.Where(s => ld.Contains(s.DirectorId) && s.MovieId == id).Select(s => s.DirectorId);
                    IEnumerable<int> na = ld.Where(s => !ed.Contains(s));
                    foreach (int di in na)
                        _context.MovieDirectors.Add(new MovieDirectors { DirectorId = di, MovieId = id });
                    await _context.SaveChangesAsync();

                    //PRODUCERS
                    IQueryable<MovieProducers> ved = _context.MovieProducers.Where(s => !pd.Contains(s.ProducerId) && s.MovieId == id);
                    _context.MovieProducers.RemoveRange(ved);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> ep = _context.MovieProducers.Where(s => pd.Contains(s.ProducerId) && s.MovieId == id).Select(s => s.ProducerId);
                    IEnumerable<int> np = pd.Where(s => !ep.Contains(s));
                    foreach (int prd in np)
                        _context.MovieProducers.Add(new MovieProducers { ProducerId = prd, MovieId = id });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(viewmodel.Movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            await _context.SaveChangesAsync();
            return View(viewmodel);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
               .Include(m => m.Genres).ThenInclude(m => m.Genres)
               .Include(m => m.Actors).ThenInclude(m => m.Actor)
               .Include(m => m.Directors).ThenInclude(m => m.Director)
               .Include(m => m.Producers).ThenInclude(m => m.Producer)
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.MovieId == id);
          
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'soprosoproContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
