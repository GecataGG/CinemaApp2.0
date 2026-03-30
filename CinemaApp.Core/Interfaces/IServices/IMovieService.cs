namespace CinemaApp.Core.Interfaces.IServices
{
    using CinemaApp.Core.ViewModels.Movie.WebMovie;

    public interface IMovieService
    {
        Task CreateMovieAsync(MovieFormModel formModel);
        Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id);
        Task<MovieFormModel?> GetMovieFormModelByIdAsync(Guid id);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync(string? userId = null);
        Task EditMovieAsync(Guid id, MovieFormModel formModel);
        Task SoftDeleteMovieAsync(Guid id);
        Task HardDeleteMovieAsync(Guid id);
    }
}