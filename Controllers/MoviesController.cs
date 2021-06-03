using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using MovieAPI.Models;
using MovieAPI.Data;

namespace MovieAPI.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private Database db = new Database();
        private List<Movie> movies = new List<Movie>();
        private List<Stats> stats = new List<Stats>();

        // GET /movies/stats
        // To get stats from the stats.csv
        [HttpGet("stats")]
        public ActionResult<List<MovieStats>> Stats()
        {            
            stats = db.getStats();
            var cal = new List<StatsCal>();

            for (int i = 0; i < stats.Count; i++)
            {
                var checker = cal.Any(x => x.MovieId == stats[i].MovieId); // Check if Movie ID exists on StatsCal
                if (!checker)
                {
                    // If Movie ID doesnt exist, added in list (Also divided Duration by 1000 to change from milliseconds to seconds)
                    cal.Add(new StatsCal(stats[i].MovieId, stats[i].WatchDurationMs / 1000)); 
                }
                else 
                {
                    StatsCal toEdit = cal.Where(x => x.MovieId == stats[i].MovieId).First(); // Find what is already in list to edit
                    cal.RemoveAll(x => x.MovieId == stats[i].MovieId); // Remove old StatsCal
                    
                    toEdit.TotalWatchDuration += stats[i].WatchDurationMs / 1000;
                    toEdit.TotalWatches += 1;
                    
                    cal.Add(toEdit); // Adds new StatsCal
                }                
            }

            var results = new List<MovieStats>();
            foreach(StatsCal c in cal)
            {
                var movie = db.getMovies(c.MovieId).FirstOrDefault(); // Using FirstOrDefault to prevent NullException

                if (movie != null)
                {
                    var average = c.TotalWatchDuration / c.TotalWatches;
                    MovieStats m = new MovieStats(c.MovieId, movie.Title, average, c.TotalWatches, movie.ReleaseYear);
                    results.Add(m);
                }
            }

            results = results
                .OrderByDescending(x => x.Watches) // Highest Watches first
                .OrderByDescending(x => x.ReleaseYear) // Highest Year first
                .ToList();

            return Ok(results);
        }
    }
}