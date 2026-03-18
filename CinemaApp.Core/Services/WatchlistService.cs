namespace CinemaApp.Core.Services
{
    using CinemaApp.Core.DTOs.Watchlist;
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Data.Repositories.Contracts;
    using Data.Models;

    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository watchlistRepository;
        private readonly IMovieRepository movieRepository;

        public WatchlistService(IWatchlistRepository watchlistRepository, IMovieRepository movieRepository)
        {
            this.watchlistRepository = watchlistRepository;
            this.movieRepository = movieRepository;
        }

        public async Task<IEnumerable<WatchlistMovieDto>> GetUserWatchlistByIdAsync(string userId)
        {
            IEnumerable<Movie> userWatchlist = (await watchlistRepository
                .GetAllUserMoviesAsync(um => um.UserId == userId))
                .Select(um => um.Movie)
                .ToArray();

            IEnumerable<WatchlistMovieDto> watchlistMoviesDto = userWatchlist
                .Select(m => new WatchlistMovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate,
                    ImageUrl = m.ImageUrl
                })
                .ToArray();

            return watchlistMoviesDto;
        }

        public async Task AddMovieToUserWatchlistAsync(string userId, Guid movieId)
        {
            UserMovie? userMovie = await watchlistRepository
                .GetUserMovieIncludeDeletedAsync(userId, movieId);

            if (userMovie != null && userMovie.IsDeleted == false)
            {
                throw new InvalidOperationException("Movie is already in the user's watchlist.");
            }

            bool movieExists = await movieRepository.ExistsByIdAsync(movieId);
            if (!movieExists)
            {
                throw new InvalidOperationException("Movie not found.");
            }

            bool successPersist = false;

            if (userMovie == null)
            {
                UserMovie newUserMovie = new UserMovie
                {
                    UserId = userId,
                    MovieId = movieId
                };

                successPersist = await watchlistRepository
                    .AddUserMovieAsync(newUserMovie);
            }
            else
            {
                userMovie.IsDeleted = false;

                successPersist = await watchlistRepository
                    .UpdateUserMovieAsync(userMovie);
            }

            if (!successPersist)
            {
                throw new Exception("Movie could not be added to watchlist.");
            }
        }

        public async Task RemoveMovieFromUserWatchlistAsync(string userId, Guid movieId)
        {
            UserMovie? userMovie = await watchlistRepository
                .GetUserMovieAsync(userId, movieId);

            if (userMovie == null)
            {
                throw new InvalidOperationException("Movie is not in the user's watchlist.");
            }

            bool successDelete = await watchlistRepository
                .SoftDeleteUserMovieAsync(userMovie);

            if (!successDelete)
            {
                throw new Exception("Movie could not be removed from watchlist.");
            }
        }

        public async Task<bool> MovieIsInUserWatchlistAsync(string userId, Guid movieId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User id cannot be null or empty.");
            }

            if (movieId == Guid.Empty)
            {
                throw new ArgumentException("Movie id cannot be empty.");
            }

            bool userWatchlistEntryExists = await watchlistRepository
                .ExistsAsync(userId, movieId);

            return userWatchlistEntryExists;
        }
    }
}