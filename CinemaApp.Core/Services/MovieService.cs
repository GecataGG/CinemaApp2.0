namespace CinemaApp.Core.Services
{
    using CinemaApp.Core.DTOs.Movie;
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Data.Repositories.Contracts;

    using Data.Models;

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IWatchlistRepository watchlistRepository;

        public const string DefaultImageUrl = "~/images/def-img.png";
        public MovieService(IMovieRepository movieRepository, IWatchlistRepository watchlistRepository)
        {
            this.movieRepository = movieRepository;
            this.watchlistRepository = watchlistRepository;
        }

        public async Task<IEnumerable<MovieAllDto>> GetAllMoviesOrderedByTitleAsync(string? userId = null)
        {
            IEnumerable<Movie> allMoviesDb = await movieRepository
                .GetAllMoviesNoTrackingWithProjectionAsync(m => new Movie()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate,
                    Director = m.Director,
                    ImageUrl = m.ImageUrl ?? DefaultImageUrl,
                });

            IEnumerable<UserMovie> allUserMovies = (await watchlistRepository
                .GetAllUserMoviesAsync(um => um.UserId == userId))
                .ToHashSet();

            IEnumerable<MovieAllDto> allMoviesDtos = allMoviesDb
                .Select(m => new MovieAllDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate,
                    Director = m.Director,
                    ImageUrl = m.ImageUrl ?? DefaultImageUrl
                })
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Genre)
                .ThenBy(m => m.Director)
                .ToArray();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                foreach (MovieAllDto movieDto in allMoviesDtos)
                {
                    movieDto.IsInUserWatchlist = allUserMovies
                        .Any(um => um.MovieId == movieDto.Id &&
                                   um.UserId.ToLowerInvariant() == userId.ToLowerInvariant());
                }
            }

            return allMoviesDtos;
        }

        public async Task CreateMovieAsync(MovieDetailsDto movieDetailsDto)
        {
            Movie newMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = movieDetailsDto.Title,
                Genre = movieDetailsDto.Genre,
                ReleaseDate = movieDetailsDto.ReleaseDate,
                Description = movieDetailsDto.Description,
                Duration = movieDetailsDto.Duration,
                Director = movieDetailsDto.Director,
                ImageUrl = string.IsNullOrWhiteSpace(movieDetailsDto.ImageUrl)
                    ? DefaultImageUrl
                    : movieDetailsDto.ImageUrl
            };

            bool successAdd = await movieRepository.AddMovieAsync(newMovie);
            if (!successAdd)
            {
                throw new Exception("Movie could not be saved.");
            }
        }

        public async Task<MovieDetailsDto?> GetMovieDetailsByIdAsync(Guid id)
        {
            Movie? movieDb = await movieRepository
                .GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                return null;
            }

            return new MovieDetailsDto
            {
                Id = movieDb.Id,
                Title = movieDb.Title,
                Genre = movieDb.Genre,
                ReleaseDate = movieDb.ReleaseDate,
                Description = movieDb.Description,
                Duration = movieDb.Duration,
                Director = movieDb.Director,
                ImageUrl = movieDb.ImageUrl ?? DefaultImageUrl
            };
        }

        public async Task<MovieDetailsDto?> GetMovieFormModelByIdAsync(Guid id)
        {
            Movie? movieDb = await movieRepository
                .GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                return null;
            }

            return new MovieDetailsDto
            {
                Id = movieDb.Id,
                Title = movieDb.Title,
                Genre = movieDb.Genre,
                ReleaseDate = movieDb.ReleaseDate,
                Description = movieDb.Description,
                Duration = movieDb.Duration,
                Director = movieDb.Director,
                ImageUrl = movieDb.ImageUrl ?? DefaultImageUrl
            };
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await movieRepository.ExistsByIdAsync(id);
        }

        public async Task EditMovieAsync(Guid id, MovieDetailsDto movieDetailsDto)
        {
            Movie? movieDb = await movieRepository
                .GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                throw new InvalidOperationException("Movie not found.");
            }

            movieDb.Title = movieDetailsDto.Title;
            movieDb.Genre = movieDetailsDto.Genre;
            movieDb.ReleaseDate = movieDetailsDto.ReleaseDate;
            movieDb.Description = movieDetailsDto.Description;
            movieDb.Duration = movieDetailsDto.Duration;
            movieDb.Director = movieDetailsDto.Director;
            movieDb.ImageUrl = string.IsNullOrWhiteSpace(movieDetailsDto.ImageUrl)
                ? DefaultImageUrl
                : movieDetailsDto.ImageUrl;

            bool editSuccess = await movieRepository.EditMovieAsync(movieDb);
            if (!editSuccess)
            {
                throw new Exception("Movie could not be updated.");
            }
        }

        public async Task SoftDeleteMovieAsync(Guid id)
        {
            Movie? movieDb = await movieRepository
                .GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                throw new InvalidOperationException("Movie not found.");
            }

            bool deleteSuccess = await movieRepository.SoftDeleteMovieAsync(movieDb);
            if (!deleteSuccess)
            {
                throw new Exception("Movie could not be deleted.");
            }
        }

        public async Task HardDeleteMovieAsync(Guid id)
        {
            Movie? movieDb = await movieRepository
                .GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                throw new InvalidOperationException("Movie not found.");
            }

            bool deleteSuccess = await movieRepository.HardDeleteMovieAsync(movieDb);
            if (!deleteSuccess)
            {
                throw new Exception("Movie could not be permanently deleted.");
            }
        }
    }
}