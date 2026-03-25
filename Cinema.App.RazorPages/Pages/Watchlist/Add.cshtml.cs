using CinemaApp.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Watchlist
{
    public class AddModel : BaseWatchlistPageModel
    {
        private readonly IWatchlistService _watchlistService;
        private readonly IMovieService _movieService;
        private readonly ILogger<AddModel> _logger;

        public AddModel(
            IWatchlistService watchlistService,
            IMovieService movieService,
            ILogger<AddModel> logger)
        {
            _watchlistService = watchlistService;
            _movieService = movieService;
            _logger = logger;
        }

        // Този метод приема параметър от Query String
        public async Task<IActionResult> OnGetAsync([FromQuery] Guid id)
        {
            _logger.LogInformation("Add to watchlist called with movie ID: {MovieId}", id);

            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid movie ID (empty GUID)");
                TempData["Error"] = "Invalid movie ID.";
                return RedirectToPage("/Movies/Index");
            }

            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User not authenticated");
                TempData["Error"] = "You need to be logged in to add movies to watchlist.";
                return RedirectToPage("/Account/Login");
            }

            try
            {
                // Проверка дали филмът съществува
                var movie = await _movieService.GetMovieDetailsByIdAsync(id);
                if (movie == null)
                {
                    _logger.LogWarning("Movie with ID {MovieId} not found", id);
                    TempData["Error"] = "Movie not found.";
                    return RedirectToPage("/Movies/Index");
                }

                // Добавяне към watchlist
                await _watchlistService.AddMovieToUserWatchlistAsync(userId, id);
                _logger.LogInformation("Movie {MovieId} added to watchlist for user {UserId}", id, userId);
                TempData["Success"] = $"'{movie.Title}' was added to your watchlist!";
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Failed to add movie to watchlist: {Message}", ex.Message);
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding movie to watchlist for movie ID: {MovieId}", id);
                TempData["Error"] = "An error occurred while adding the movie to your watchlist.";
            }

            // Връщане към предишната страница
            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToPage("/Watchlist/Index");
        }
    }
}