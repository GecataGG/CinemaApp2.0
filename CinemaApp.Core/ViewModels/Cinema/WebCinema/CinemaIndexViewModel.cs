namespace CinemaApp.Core.ViewModels.Cinema.WebCinema
{
    using CinemaApp.Core.DTOs.Cinema;

    public class CinemaIndexViewModel
    {
        public IEnumerable<CinemaAllDto> Cinemas { get; set; } = new List<CinemaAllDto>();
    }
}