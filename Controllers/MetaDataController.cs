using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using MovieAPI.Models;
using MovieAPI.Data;

namespace MovieAPI.Controllers
{
    [Route("metadata")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private Database db = new Database();
        private List<Movie> movies = new List<Movie>();

        [HttpPost]
        public ActionResult<Movie> CreateMovie(Movie movie)
        {
            db.addMovie(movie);
            return Created("/", movie);
        }

        [HttpGet]
        public ActionResult <List<Movie>> ReadMovie()
        {
            movies = db.getMovies();
            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public ActionResult<List<Movie>> ReadMovieByMovieId(int movieId)
        {
            movies = db.getMovies(movieId);
            
            if(movies.Count == 0)
            {
                return NotFound();
            }

            var result = movies.GroupBy(x => x.Title.ToLower()).Select(y => y.Last()).OrderBy(z => z.Language);
            return Ok(result);
        }
    }
}