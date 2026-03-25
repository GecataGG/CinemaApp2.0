using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Movie.WebMovie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Movies
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IMovieService _movieService;

        public DeleteModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public MovieDeleteViewModel? Movie { get; set; }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var movieDetailsVm = await _movieService.GetMovieDetailsByIdAsync(id);

            if (movieDetailsVm == null)
            {
                return NotFound();
            }

            Movie = new MovieDeleteViewModel
            {
                Id = movieDetailsVm.Id,
                Title = movieDetailsVm.Title,
                ImageUrl = movieDetailsVm.ImageUrl
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _movieService.SoftDeleteMovieAsync(id);

            TempData["Success"] = "Movie deleted successfully!";
            return RedirectToPage("/Movies/Index");
        }
    }
}