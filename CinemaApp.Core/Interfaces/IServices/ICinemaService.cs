namespace CinemaApp.Core.Interfaces.IServices
{
    using CinemaApp.Core.DTOs.Cinema;

    public interface ICinemaService
    {
        Task<IEnumerable<CinemaAllDto>> GetAllCinemasOrderedByLocationAsync();

        Task<CinemaProgramDetailsDto?> GetCinemaProgramByIdAsync(Guid cinemaId);
    }
}