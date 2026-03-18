namespace CinemaApp.Core.Interfaces.IServices
{
    using CinemaApp.Core.DTOs.Movie;

    public interface IMovieService
    {
        Task<IEnumerable<MovieAllDto>> GetAllMoviesOrderedByTitleAsync(string? userId = null);

        Task CreateMovieAsync(MovieDetailsDto movieDetailsDto);

        Task<MovieDetailsDto?> GetMovieDetailsByIdAsync(Guid id);

        Task<MovieDetailsDto?> GetMovieFormModelByIdAsync(Guid id);

        Task<bool> ExistsByIdAsync(Guid id);

        Task EditMovieAsync(Guid id, MovieDetailsDto movieDetailsDto);

        Task SoftDeleteMovieAsync(Guid id);

        Task HardDeleteMovieAsync(Guid id);
    }
}
