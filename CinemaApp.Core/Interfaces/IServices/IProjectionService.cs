namespace CinemaApp.Core.Interfaces.IServices
{
    using CinemaApp.Core.DTOs.Projection;

    public interface IProjectionService
    {
        Task<IEnumerable<ProjectionShowtimeDto>> GetProjectionAvailableShowtimesAsync(Guid movieId, Guid cinemaId);
    }
}
