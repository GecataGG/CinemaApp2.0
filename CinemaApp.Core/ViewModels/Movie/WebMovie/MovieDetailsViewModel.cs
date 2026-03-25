namespace CinemaApp.Core.ViewModels.Movie.WebMovie
{
    using CinemaApp.Core.DTOs.Movie;

    public class MovieDetailsViewModel : AllMoviesIndexViewModel
    {
        public string Description { get; set; } = null!;
        public int Duration { get; set; }

    }
}