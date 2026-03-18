namespace CinemaApp.Core.DTOs.Movie
{
    public class MovieAllDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public string Director { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool IsInUserWatchlist { get; set; }
    }
}
