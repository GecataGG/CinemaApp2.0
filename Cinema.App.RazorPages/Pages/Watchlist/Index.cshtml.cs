using CinemaApp.Core.DTOs.Watchlist;
using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Watchlist.WebWatchlist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Watchlist
{
    public class IndexModel : BaseWatchlistPageModel
    {
        private readonly IWatchlistService _watchlistService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IWatchlistService watchlistService, ILogger<IndexModel> logger)
        {
            _watchlistService = watchlistService;
            _logger = logger;
        }

        public IEnumerable<WatchlistMovieViewModel> WatchlistMovies { get; set; }
            = new List<WatchlistMovieViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = GetUserId()!;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            try
            {
                IEnumerable<WatchlistMovieDto> watchlistMovieDtos =
                    await _watchlistService.GetUserWatchlistByIdAsync(userId);

                WatchlistMovies = watchlistMovieDtos
                    .Select(w => new WatchlistMovieViewModel
                    {
                        MovieId = w.Id,
                        Title = w.Title,
                        Genre = w.Genre,
                        ReleaseDate = w.ReleaseDate.ToString("yyyy-MM-dd"),
                        ImageUrl = w.ImageUrl
                    })
                    .ToList();

                _logger.LogInformation("Loaded {Count} movies for user {UserId}", WatchlistMovies.Count(), userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading watchlist for user {UserId}", userId);
                TempData["Error"] = "Error loading watchlist.";
            }

            return Page();
        }
    }
}