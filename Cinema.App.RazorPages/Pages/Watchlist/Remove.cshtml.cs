using CinemaApp.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Watchlist
{
    public class RemoveModel : BaseWatchlistPageModel
    {
        private readonly IWatchlistService _watchlistService;
        private readonly IMovieService _movieService;
        private readonly ILogger<RemoveModel> _logger;

        public RemoveModel(
            IWatchlistService watchlistService,
            IMovieService movieService,
            ILogger<RemoveModel> logger)
        {
            _watchlistService = watchlistService;
            _movieService = movieService;
            _logger = logger;
        }

        // Използваме FromRoute за ID от URL-то
        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            _logger.LogInformation("Remove from watchlist called with movie ID: {MovieId}", id);

            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid movie ID (empty GUID)");
                TempData["Error"] = "Invalid movie ID.";
                return RedirectToPage("/Watchlist/Index");
            }

            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User not authenticated");
                TempData["Error"] = "You need to be logged in to remove movies from watchlist.";
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
                    return RedirectToPage("/Watchlist/Index");
                }

                await _watchlistService.RemoveMovieFromUserWatchlistAsync(userId, id);
                _logger.LogInformation("Movie {MovieId} removed from watchlist for user {UserId}", id, userId);
                TempData["Success"] = $"'{movie.Title}' was removed from your watchlist!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing movie from watchlist for movie ID: {MovieId}", id);
                TempData["Error"] = "Error while removing movie from watchlist.";
            }

            return RedirectToPage("/Watchlist/Index");
        }

        // POST метод за безопасност
        public async Task<IActionResult> OnPostAsync([FromForm] Guid movieId)
        {
            return await OnGetAsync(movieId);
        }
    }
}