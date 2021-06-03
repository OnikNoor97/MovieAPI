namespace MovieAPI.Models
{
    public class StatsCal
    {
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