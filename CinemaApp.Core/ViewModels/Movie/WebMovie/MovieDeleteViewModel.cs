namespace CinemaApp.Core.ViewModels.Movie.WebMovie
{
    public class MovieDeleteViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
