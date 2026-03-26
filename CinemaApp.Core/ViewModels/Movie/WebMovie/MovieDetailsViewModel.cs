namespace CinemaApp.Core.ViewModels.Movie.WebMovie
{
    public class MovieDetailsViewModel : AllMoviesIndexViewModel
    {
        public string Description { get; set; } = null!;
        public int Duration { get; set; }

    }
}