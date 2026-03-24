namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Core.DTOs.Watchlist;
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Core.ViewModels.Watchlist.WebWatchlist;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;
        private readonly ILogger<WatchlistController> logger;

        public WatchlistController(IWatchlistService watchlistService, ILogger<WatchlistController> logger)
        {
            this.watchlistService = watchlistService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.GetUserId()!;

            IEnumerable<WatchlistMovieDto> watchlistMovieDtos =
                await this.watchlistService.GetUserWatchlistByIdAsync(userId);

            IEnumerable<WatchlistMovieViewModel> watchlistMovieViewModels = watchlistMovieDtos
                .Select(w => new WatchlistMovieViewModel
                {
                    MovieId = w.Id,
                    Title = w.Title,
                    Genre = w.Genre,
                    ReleaseDate = w.ReleaseDate.ToString("yyyy-MM-dd"),
                    ImageUrl = w.ImageUrl
                })
                .ToList();

            return this.View(watchlistMovieViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Add([FromRoute(Name = "id")] Guid movieId)
        {
            string userId = this.GetUserId()!;

            try
            {
                await this.watchlistService.AddMovieToUserWatchlistAsync(userId, movieId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while adding movie to watchlist.");
                this.TempData["Error"] = "Error while adding movie to watchlist.";

                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid movieId)
        {
            string userId = this.GetUserId()!;

            try
            {
                await this.watchlistService.RemoveMovieFromUserWatchlistAsync(userId, movieId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while removing movie from watchlist.");
                this.TempData["Error"] = "Error while removing movie from watchlist.";

                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }
    }
}
