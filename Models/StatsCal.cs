namespace MovieAPI.Models
{
    public class StatsCal
    {
        // Purpose of this Class is to combine both Movie and Stats class using the stats.csv file
        // There may be a better way to handle many to many relationships in terms of code
        public int MovieId { get; set; }
        public int TotalWatchDuration { get; set; }
        public int TotalWatches {get; set; }

        public StatsCal(int movieId, int totalWatchDuration)
        {
            this.MovieId = movieId;
            this.TotalWatchDuration = totalWatchDuration;
            this.TotalWatches = 1;
        }
    }
}