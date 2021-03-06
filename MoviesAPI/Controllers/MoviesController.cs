﻿using System;
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

        private enum Result
        {
            BadRequest,
            NoData,
            Ok
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

        private IQueryable<Movie> GetMovieByFilter(IQueryable<Movie> movies, string filter, string value, out Result result)
        {
            result = Result.NoData;
            if (movies.Count() == 0)
                return movies;

            if (filter.Equals(FilterCriteria.Title.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                movies = movies.Where(m => m.Title.ToLower().Contains(value.ToLower()));
            }
            else if (filter.Equals(FilterCriteria.YearOfRelease.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                int year;
                if (int.TryParse(value, out year))
                {
                    movies = movies.Where(m => m.YearOfRelease == year);
                }
                else
                {
                    result = Result.BadRequest;
                }
            }
            else if (filter.Equals(FilterCriteria.Genre.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                string[] genres = value.ToLower().Split(',');
                movies = movies.SelectMany(m => m.Genres).Where(g => genres.Contains(g.Name)).SelectMany(g => g.Movies);
            }
            else
            {
                result = Result.BadRequest;
            }
            return movies;
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
            new SummarizedMovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                YearOfRelease = m.YearOfRelease,
                RunningTime = m.RunningTime,
                AverageRating = m.Ratings.Average(r => r.Score)
            });

            return Ok(movieDTOList);
        }

        [Route("{filter1}/{value1}/{filter2}/{value2}")]
        public IHttpActionResult GetMovie(string filter1, string value1, string filter2, string value2)
        {
            IQueryable<Movie> movies = null;
            if (filter1.Equals(FilterCriteria.Title.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                movies = db.Movies.Include(m => m.Genres).Include(m => m.Ratings).Where(m => m.Title.ToLower().Contains(value1.ToLower()));
            }
            else if (filter1.Equals(FilterCriteria.YearOfRelease.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                int year;
                if (int.TryParse(value1, out year))
                {
                    movies = db.Movies.Include(m => m.Genres).Include(m => m.Ratings).Where(m => m.YearOfRelease == year);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (filter1.Equals(FilterCriteria.Genre.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                string[] genres = value1.ToLower().Split(',');
                movies = db.Genres.Include(g => g.Movies).Where(g => genres.Contains(g.Name.ToLower())).SelectMany(g => g.Movies);
            }
            else
            {
                return BadRequest();
            }

            Result result;
            movies = GetMovieByFilter(movies, filter2, value2, out result);

            if (result == Result.BadRequest)
                return BadRequest();

            if (movies.Count() == 0)
                return NotFound();

            var movieDTOList = movies.Select(m =>
            new SummarizedMovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                YearOfRelease = m.YearOfRelease,
                RunningTime = m.RunningTime,
                AverageRating = m.Ratings.Average(r => r.Score)
            });

            return Ok(movieDTOList);
        }

        [Route("{filter1}/{value1}/{filter2}/{value2}/{filter3}/{value3}")]
        public IHttpActionResult GetMovie(string filter1, string value1, string filter2, string value2, string filter3, string value3)
        {
            IQueryable<Movie> movies = null;
            if (filter1.Equals(FilterCriteria.Title.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                movies = db.Movies.Include(m => m.Genres).Include(m => m.Ratings).Where(m => m.Title.ToLower().Contains(value1.ToLower()));
            }
            else if (filter1.Equals(FilterCriteria.YearOfRelease.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                int year;
                if (int.TryParse(value1, out year))
                {
                    movies = db.Movies.Include(m => m.Genres).Include(m => m.Ratings).Where(m => m.YearOfRelease == year);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (filter1.Equals(FilterCriteria.Genre.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                string[] genres = value1.ToLower().Split(',');
                movies = db.Genres.Include(g => g.Movies).Where(g => genres.Contains(g.Name.ToLower())).SelectMany(g => g.Movies);
            }
            else
            {
                return BadRequest();
            }

            Result result;
            movies = GetMovieByFilter(movies, filter2, value2, out result);

            if (result == Result.BadRequest)
                return BadRequest();

            movies = GetMovieByFilter(movies, filter3, value3, out result);

            if (result == Result.BadRequest)
                return BadRequest();

            if (movies.Count() == 0)
                return NotFound();

            var movieDTOList = movies.Select(m =>
            new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                YearOfRelease = m.YearOfRelease,
                RunningTime = m.RunningTime,
                AverageRating = m.Ratings.Average(r => r.Score)
            });

            return Ok(movieDTOList);
        }

        [Route("top5")]
        public IHttpActionResult GetTop5Movies()
        {
            IQueryable<Movie> movies = db.Movies.Include(m => m.Genres).Include(m => m.Ratings).OrderByDescending(m => m.Ratings.Average(r => r.Score)).Take(5);

            var movieDTOList = movies.Select(m =>
            new SummarizedMovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                YearOfRelease = m.YearOfRelease,
                RunningTime = m.RunningTime,
                AverageRating = m.Ratings.Average(r => r.Score)
            });

            return Ok(movieDTOList);
        }

        [Route("top5/{userName}")]
        public IHttpActionResult GetTop5MoviesByUserName(string userName)
        {
            var userId = db.Users.Include(u => u.Ratings).SingleOrDefault(u => u.UserName == userName).Id;

            var scoredMovies = db.Ratings.Include(r => r.Movie).Where(r => r.UserId == userId).GroupBy(x => x.Movie)
                .Select(g => new
                {
                    Movie = g.Key,
                    AvgScore = g.Average(x => x.Score)
                }).OrderByDescending(x => x.AvgScore).Take(5);

            var movieDTOList = scoredMovies.Select(x =>
            new SummarizedMovieDTO
            {
                Id = x.Movie.Id,
                Title = x.Movie.Title,
                YearOfRelease = x.Movie.YearOfRelease,
                RunningTime = x.Movie.RunningTime,
                AverageRating = x.AvgScore
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