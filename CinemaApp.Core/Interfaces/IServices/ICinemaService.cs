namespace CinemaApp.Core.Interfaces.IServices
{
    using CinemaApp.Core.ViewModels.Cinema.WebCinema;

    public interface ICinemaService
    {
        Task<IEnumerable<CinemaIndexViewModel>> GetAllCinemasOrderedByLocationAsync();
        Task<CinemaProgramViewModel> GetCinemaProgramByIdAsync(Guid cinemaId);
    }
}
