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

        // Please ignore this GET, I added it for sanity check to ensure CSV to JSON is working correctly
        [HttpGet]
        public ActionResult <List<Movie>> ReadMovie()
        {
            movies = db.getMovies();
            return Ok(movies);
        }

        // GET /metadata/:movieId
        // To get a list of movies with the id specified
        [HttpGet("{movieId}")]
        public ActionResult<List<Movie>> ReadMovieByMovieId(int movieId)
        {
            movies = db.getMovies(movieId);
            
            if(movies.Count == 0)
            {
                return NotFound(); // If there are no movies with the Movie ID provided, not found is shown to the client
            }

            var result = movies
                .GroupBy(x => x.Title.ToLower()) // Group by name - Note: Added toLower() as I noticed there are some titles with captials
                .Select(y => y.Last()) // Get the last in the group i.e. Highest ID in the group
                .OrderBy(z => z.Language); // Order by Language Alphabetically

            return Ok(result);
        }
    }
}