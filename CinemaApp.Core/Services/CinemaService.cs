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
            IEnumerable<Cinema> allCinemas = (await cinemaRepository
                .GetAllCinemas(
                    filterQuery: null,
                    projectionQuery: c => new Cinema
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Location = c.Location
                    }))
                .OrderBy(c => c.Location)
                .ToArray();

            IEnumerable<CinemaAllDto> cinemaDtos = allCinemas
                .Select(c => new CinemaAllDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Location = c.Location
                })
                .ToArray();

            return cinemaDtos;
        }

        public async Task<CinemaProgramDetailsDto?> GetCinemaProgramByIdAsync(Guid cinemaId)
        {
            Cinema? cinema = await cinemaRepository
                .GetCinemaByIdIncludeMovies(cinemaId);

            if (cinema == null)
            {
                return null;
            }

            CinemaProgramDetailsDto cinemaProgramDto = new CinemaProgramDetailsDto
            {
                Id = cinema.Id,
                Name = cinema.Name,
                ProjectionMovies = cinema.Projections
                    .Select(p => p.Movie)
                    .Where(m => m != null)
                    .GroupBy(m => m.Id)
                    .Select(g => new CinemaProgramMovieDto
                    {
                        Id = g.First().Id,
                        Title = g.First().Title
                    })
                    .ToList()
            };

            return cinemaProgramDto;
        }
    }
}