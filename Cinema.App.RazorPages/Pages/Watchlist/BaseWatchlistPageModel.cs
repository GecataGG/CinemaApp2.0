using Cinema.App.RazorPages.Pages;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.App.RazorPages.Pages.Watchlist
{
    [Authorize]
    public class BaseWatchlistPageModel : BasePageModel
    {
        // Базов клас за всички watchlist страници
    }
}