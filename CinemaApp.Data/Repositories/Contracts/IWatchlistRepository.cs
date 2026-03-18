namespace CinemaApp.Data.Repositories.Contracts
{
    using System.Linq.Expressions;

    using Models;

    public interface IWatchlistRepository
    {
        Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync(Expression<Func<UserMovie, bool>>? filterQuery = null);

        Task<UserMovie?> GetUserMovieAsync(string userId, Guid movieId);

        Task<UserMovie?> GetUserMovieIncludeDeletedAsync(string userId, Guid movieId);

        Task<bool> ExistsAsync(string userId, Guid movieId);

        Task<bool> AddUserMovieAsync(UserMovie userMovie);

        Task<bool> UpdateUserMovieAsync(UserMovie userMovie);

        Task<bool> SoftDeleteUserMovieAsync(UserMovie userMovie);
    }
}
