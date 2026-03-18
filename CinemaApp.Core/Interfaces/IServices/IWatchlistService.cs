namespace CinemaApp.Core.Interfaces.IServices
{
    using CinemaApp.Core.DTOs.Watchlist;

    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistMovieDto>> GetUserWatchlistByIdAsync(string userId);

        Task AddMovieToUserWatchlistAsync(string userId, Guid movieId);

        Task RemoveMovieFromUserWatchlistAsync(string userId, Guid movieId);

        Task<bool> MovieIsInUserWatchlistAsync(string userId, Guid movieId);
    }
}
