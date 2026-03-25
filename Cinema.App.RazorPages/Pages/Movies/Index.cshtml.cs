using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Movie.WebMovie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Movies
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IMovieService _movieService;

        public IndexModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IEnumerable<AllMoviesIndexViewModel> Movies { get; set; }
            = new List<AllMoviesIndexViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            string? userId = User?.Identity?.IsAuthenticated == true
                ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                : null;

            Movies = await _movieService.GetAllMoviesOrderedByTitleAsync(userId);

            return Page();
        }
    }
}