namespace CinemaApp.Core.DTOs.Watchlist
{
    public class WatchlistMovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
