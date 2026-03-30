namespace CinemaApp.Core.Services
{
    using CinemaApp.Core.DTOs.Movie;
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Core.ViewModels.Movie.WebMovie;
    using CinemaApp.Data.Repositories.Contracts;
    using CinemaApp.Data.Models;

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

        public async Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id)
        {
            Movie? movieDb = await movieRepository
                .GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                return null;
            }

            return new MovieDetailsViewModel
            {
                Id = movieDb.Id,
                Title = movieDb.Title,
                Genre = movieDb.Genre,
                ReleaseDate = movieDb.ReleaseDate.ToString("yyyy-MM-dd"),
                Director = movieDb.Director,
                Description = movieDb.Description,
                Duration = movieDb.Duration,
                ImageUrl = movieDb.ImageUrl ?? DefaultImageUrl,
                IsInUserWatchlist = false
            };
        }

        public async Task<MovieFormModel?> GetMovieFormModelByIdAsync(Guid id)
        {
            Movie? movieDb = await movieRepository
                .GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                return null;
            }

            return new MovieFormModel
            {
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

        public async Task EditMovieAsync(Guid id, MovieFormModel formModel)
        {
            Movie? movieDb = await movieRepository
                .GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                throw new InvalidOperationException("Movie not found.");
            }

            movieDb.Title = formModel.Title;
            movieDb.Genre = formModel.Genre;
            movieDb.ReleaseDate = formModel.ReleaseDate;
            movieDb.Description = formModel.Description;
            movieDb.Duration = formModel.Duration;
            movieDb.Director = formModel.Director;
            movieDb.ImageUrl = string.IsNullOrWhiteSpace(formModel.ImageUrl)
                ? DefaultImageUrl
                : formModel.ImageUrl;

            bool editSuccess = await movieRepository.EditMovieAsync(movieDb);
            if (!editSuccess)
            {
                throw new Exception("Movie could not be updated.");
            }
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync(string? userId = null)
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

            // Първо създаваме DTO-та за вътрешна обработка
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

            // Мапваме DTO-та към ViewModel-и
            return allMoviesDtos.Select(dto => new AllMoviesIndexViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate.ToString("yyyy-MM-dd"),
                Director = dto.Director,
                ImageUrl = dto.ImageUrl,
                IsInUserWatchlist = dto.IsInUserWatchlist
            }).ToList();
        }

        public async Task CreateMovieAsync(MovieFormModel formModel)
        {
            Movie newMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = formModel.Title,
                Genre = formModel.Genre,
                ReleaseDate = formModel.ReleaseDate,
                Description = formModel.Description,
                Duration = formModel.Duration,
                Director = formModel.Director,
                ImageUrl = string.IsNullOrWhiteSpace(formModel.ImageUrl)
                    ? DefaultImageUrl
                    : formModel.ImageUrl
            };

            bool successAdd = await movieRepository.AddMovieAsync(newMovie);
            if (!successAdd)
            {
                throw new Exception("Movie could not be saved.");
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