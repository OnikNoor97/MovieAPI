namespace MovieAPI.Models
{
    public class MovieStats
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int AverageWatchDurationS { get; set; } // Going to assume S is seconds due to Int32 Max Limit 
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }

        public MovieStats(int movieId, string title, int averageWatchDurationS, int watches, int releaseYear)
        {
            this.MovieId = movieId;
            this.Title = title;
            this.AverageWatchDurationS = averageWatchDurationS;
            this.Watches = watches;
            this.ReleaseYear = releaseYear;
        }
    }
}