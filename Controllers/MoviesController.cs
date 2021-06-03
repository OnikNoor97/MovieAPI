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

        [HttpGet("stats")]
        public ActionResult<List<MovieStats>> Stats()
        {            
            stats = db.getStats();
            var cal = new List<StatsCal>();

            for (int i = 0; i < stats.Count; i++)
            {
                var checker = cal.Any(x => x.MovieId == stats[i].MovieId);
                if (!checker)
                {
                    cal.Add(new StatsCal(stats[i].MovieId, stats[i].WatchDurationMs / 1000));
                }
                else 
                {
                    StatsCal toEdit = cal.Where(x => x.MovieId == stats[i].MovieId).First();
                    cal.RemoveAll(x => x.MovieId == stats[i].MovieId);
                    
                    toEdit.TotalWatchDuration += stats[i].WatchDurationMs / 1000;
                    toEdit.TotalWatches += 1;
                    
                    cal.Add(toEdit);
                }                
            }

            var results = new List<MovieStats>();
            foreach(StatsCal c in cal)
            {
                var movie = db.getMovies(c.MovieId).FirstOrDefault();

                if (movie != null)
                {
                    var average = c.TotalWatchDuration / c.TotalWatches;
                    MovieStats m = new MovieStats(c.MovieId, movie.Title, average, c.TotalWatches, movie.ReleaseYear);
                    results.Add(m);
                }
            }

            results = results
                .OrderByDescending(x => x.Watches)
                .OrderByDescending(x => x.ReleaseYear)
                .ToList();

            return Ok(results);
        }
    }
}