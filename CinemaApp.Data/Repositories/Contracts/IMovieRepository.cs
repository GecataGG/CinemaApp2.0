namespace CinemaApp.Data.Repositories.Contracts
{
    using System.Linq.Expressions;
    using CinemaApp.Data.Models;

    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesNoTrackingWithProjectionAsync(Expression<Func<Movie, Movie>>? projectionQuery = null);

        Task<IEnumerable<Movie>> GetAllMoviesAsync();

        Task<Movie?> GetMovieByIdAsync(Guid id);

        Task<bool> AddMovieAsync(Movie movie);

        Task<bool> EditMovieAsync(Movie movie);

        Task<bool> SoftDeleteMovieAsync(Movie movie);

        Task<bool> HardDeleteMovieAsync(Movie movie);

        Task<bool> ExistsByIdAsync(Guid id);
    }
}