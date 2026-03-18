namespace CinemaApp.Core.ViewModels.Movie
{
    public class MovieDeleteViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
