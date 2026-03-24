namespace CinemaApp.Core.ViewModels.Cinema.WebCinema
{
    public class CinemaProgramViewModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<CinemaProgramMoviesViewModel> ProjectionMovies { get; set; }
            = new List<CinemaProgramMoviesViewModel>();
    }
}
