namespace CinemaApp.Data.Repositories.Contracts
{
    using CinemaApp.Data.Models;
    using System.Linq.Expressions;
    public interface ICinemaRepository
    {
        Task<IEnumerable<Cinema>> GetAllCinemas(Expression<Func<Cinema, bool>>? filterQuery = null,
            Expression<Func<Cinema, Cinema>>? projectionQuery = null, bool includeProjections = false);

        Task<Cinema?> GetCinemaByIdIncludeMovies(Guid cinemaId);
    }
}