using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Movie.WebMovie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Movies
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMovieService _movieService;

        public CreateModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public MovieFormModel MovieForm { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _movieService.CreateMovieAsync(MovieForm);

            TempData["Success"] = "Movie created successfully!";
            return RedirectToPage("/Movies/Index");
        }
    }
}