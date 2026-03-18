namespace CinemaApp.Core.Services
{
    using CinemaApp.Core.DTOs.Projection;
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Data.Repositories.Contracts;
    using Data.Models;

    public class ProjectionService : IProjectionService
    {
        private readonly IProjectionRepository projectionRepository;

        public ProjectionService(IProjectionRepository projectionRepository)
        {
            this.projectionRepository = projectionRepository;
        }

        public async Task<IEnumerable<ProjectionShowtimeDto>> GetProjectionAvailableShowtimesAsync(Guid movieId, Guid cinemaId)
        {
            IEnumerable<Projection> projections = await projectionRepository
                .GetAllProjectionsAsync(pr =>
                    pr.MovieId == movieId &&
                    pr.CinemaId == cinemaId &&
                    pr.AvailableTickets > 0);

            IEnumerable<ProjectionShowtimeDto> projectionShowtimes = projections
                .Select(pr => new ProjectionShowtimeDto
                {
                    Id = pr.Id,
                    StartTime = pr.Showtime,
                    AvailableTickets = pr.AvailableTickets
                })
                .ToList();

            return projectionShowtimes;
        }
    }
}