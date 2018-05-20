using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MoviesAPI.Models;
using MoviesAPI.Models.DTO;

namespace MoviesAPI.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        private enum FilterCriteria
        {
            Title,
            YearOfRelease,
            Genre
        }

        private MoviesAPIContext db = new MoviesAPIContext();

        // GET: api/Movies
        [Route("")]
        public IQueryable<MovieDTO> GetMovies()
        {
            var movies = from m in db.Movies
                         select new MovieDTO
                         {
                             Id = m.Id,
                             Title = m.Title,
                             RunningTime = m.RunningTime,
                             YearOfRelease = m.YearOfRelease
                         };
            return movies;
        }

        [Route("{id:int}")]
        [ResponseType(typeof(MovieDTO))]
        public async Task<IHttpActionResult> GetMovie(int id)
        {
            Movie movie = await db.Movies.Include(m => m.Genres).SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(new MovieDTO(movie));
        }

        [Route("{filter}/{value}")]
        public IHttpActionResult GetMovie(string filter, string value)
        {
            IQueryable<Movie> movies = null;
            if (filter.Equals(FilterCriteria.Title.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                movies = db.Movies.Include(m => m.Genres).Include(m => m.Ratings).Where(m => m.Title.ToLower().Contains(value.ToLower()));
            }
            else if (filter.Equals(FilterCriteria.YearOfRelease.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                int year;
                if (int.TryParse(value, out year))
                {
                    movies = db.Movies.Include(m => m.Genres).Include(m => m.Ratings).Where(m => m.YearOfRelease == year);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (filter.Equals(FilterCriteria.Genre.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                string[] genres = value.ToLower().Split(',');
                movies = db.Genres.Include(g => g.Movies).Where(g => genres.Contains(g.Name.ToLower())).SelectMany(g => g.Movies);
            }
            else
            {
                return BadRequest();
            }

            var movieDTOList = movies.Select(m =>
            new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                YearOfRelease = m.YearOfRelease,
                RunningTime = m.RunningTime,
                AverageRating = m.Ratings.Average(r => r.Score),
                Genres = m.Genres.Select(g => g.Name).ToList()
            });

            return Ok(movieDTOList);
        }
        
        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movie);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> DeleteMovie(int id)
        {
            Movie movie = await db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.Id == id) > 0;
        }

        public IQueryable<MovieDTO> GetMovies(string filterCriteria, string value)
        {
            var movies = from m in db.Movies
                         select new MovieDTO
                         {
                             Id = m.Id,
                             Title = m.Title,
                             RunningTime = m.RunningTime,
                             YearOfRelease = m.YearOfRelease
                         };
            return movies;
        }
    }
}