using System.IO;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
    
using CsvHelper;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class Database
    {
        public string MetadataPath = $"{Directory.GetCurrentDirectory()}/Data/metadata.csv";
        public string RawMetaDataPath = $"{Directory.GetCurrentDirectory()}/Data/rawmetadata.csv";
        public string StatsPath = $"{Directory.GetCurrentDirectory()}/Data/stats.csv";

        public List<Movie> getMovies(int? id = null) 
        {
            List<Movie> movies = new List<Movie>();

            using (var steamReader = new StreamReader(MetadataPath))
            {
                using (var csvReader = new CsvReader(steamReader, CultureInfo.InvariantCulture))
                {
                    movies = csvReader.GetRecords<Movie>().ToList();
                }
            }

            return id == null ? movies : movies.Where(x => x.MovieId == id).ToList();
        }
        public void addMovie(Movie movie)
        {
            List<Movie> movies = this.getMovies();

            movie.Id = movies.Last().Id + 1;
            movies.Add(movie);

            using (StreamWriter streamWriter = new StreamWriter(MetadataPath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(movies);
                }
            }
        }

        public List<Stats> getStats()
        {
            List<Stats> stats = new List<Stats>();

            using (var steamReader = new StreamReader(StatsPath))
            {
                using (var csvReader = new CsvReader(steamReader, CultureInfo.InvariantCulture))
                {
                    stats = csvReader.GetRecords<Stats>().ToList();
                }
            }

            return stats;
        }
    }
}