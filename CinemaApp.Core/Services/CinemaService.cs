namespace CinemaApp.Core.Services
{
    using CinemaApp.Core.DTOs.Cinema;
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repositories.Contracts;

    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<CinemaAllDto>> GetAllCinemasOrderedByLocationAsync()
        {
            // Използваме съществуващия метод GetAllCinemas без филтри
            IEnumerable<Cinema> allCinemas = await cinemaRepository
                .GetAllCinemas(
                    filterQuery: null,
                    projectionQuery: null,
                    includeProjections: false);

            // Мапване към DTO и подреждане по локация
            return allCinemas
                .Where(c => !c.IsDeleted) // Само неизтритите
                .OrderBy(c => c.Location)
                .Select(c => new CinemaAllDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Location = c.Location
                })
                .ToList();
        }

        public async Task<CinemaProgramDetailsDto?> GetCinemaProgramByIdAsync(Guid cinemaId)
        {
            Cinema? cinema = await cinemaRepository
                .GetCinemaByIdIncludeMovies(cinemaId);

            if (cinema == null || cinema.IsDeleted)
            {
                return null;
            }

            // Създаване на DTO
            CinemaProgramDetailsDto dto = new CinemaProgramDetailsDto
            {
                Id = cinema.Id,
                Name = cinema.Name,
                ProjectionMovies = cinema.Projections
                    .Where(p => p.Movie != null && !p.Movie.IsDeleted)
                    .Select(p => p.Movie)
                    .DistinctBy(m => m.Id)
                    .Select(m => new CinemaProgramMovieDto
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Director = m.Director,
                        ImageUrl = m.ImageUrl ?? string.Empty
                    })
                    .ToList()
            };

            return dto;
        }
    }
}