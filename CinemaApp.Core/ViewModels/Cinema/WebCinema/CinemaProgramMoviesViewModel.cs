namespace CinemaApp.Core.ViewModels.Cinema.WebCinema
{
    public class CinemaProgramMoviesViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
