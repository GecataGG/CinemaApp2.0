using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Movie.WebMovie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Movies
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IMovieService _movieService;

        public EditModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //connect UI with the form model
        [BindProperty]
        public MovieFormModel MovieForm { get; set; } = new();

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id) //Movies/Edit/3f29c8a2-1234
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var formModel = await _movieService.GetMovieFormModelByIdAsync(id);

            if (formModel == null)
            {
                return NotFound();
            }

            MovieForm = formModel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _movieService.EditMovieAsync(id, MovieForm);

            TempData["Success"] = "Movie updated successfully!";
            return RedirectToPage("/Movies/Details", new { id });
        }
    }
}