using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Movie.WebMovie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Movies
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly IMovieService _movieService;

        public DetailsModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public MovieDetailsViewModel? Movie { get; set; }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();    
            }

            Movie = await _movieService.GetMovieDetailsByIdAsync(id);

            if (Movie == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}