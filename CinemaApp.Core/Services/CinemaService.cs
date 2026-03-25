namespace CinemaApp.Core.Services
{
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Core.ViewModels.Cinema.WebCinema;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repositories.Contracts;

    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<CinemaIndexViewModel>> GetAllCinemasOrderedByLocationAsync()
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

            // Директно мапване към ViewModel
            return allCinemas.Select(c => new CinemaIndexViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location
            }).ToList();
        }

        public async Task<CinemaProgramViewModel?> GetCinemaProgramByIdAsync(Guid cinemaId)
        {
            Cinema? cinema = await cinemaRepository
                .GetCinemaByIdIncludeMovies(cinemaId);

            if (cinema == null)
            {
                return null;
            }

            // Директно създаване на ViewModel в Service-а
            CinemaProgramViewModel viewModel = new CinemaProgramViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                ProjectionMovies = cinema.Projections
                    .Select(p => p.Movie)
                    .Where(m => m != null)
                    .GroupBy(m => m.Id)
                    .Select(g => new CinemaProgramMoviesViewModel
                    {
                        Id = g.First().Id,
                        Title = g.First().Title,
                        Director = g.First().Director,
                        ImageUrl = g.First().ImageUrl ?? string.Empty
                    })
                    .ToList()
            };

            return viewModel;
        }
    }
}