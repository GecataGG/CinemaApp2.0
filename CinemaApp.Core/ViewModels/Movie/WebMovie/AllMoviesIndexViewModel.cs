namespace CinemaApp.Core.ViewModels.Movie.WebMovie
{
    public class AllMoviesIndexViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool IsInUserWatchlist { get; set; }
    }
}
