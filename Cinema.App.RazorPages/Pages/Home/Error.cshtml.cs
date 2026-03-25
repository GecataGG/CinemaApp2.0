using CinemaApp.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Cinema.App.RazorPages.Pages.Home
{
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public ErrorViewModel ErrorViewModel { get; set; } = new();

        public IActionResult OnGet(int statusCode = 0)
        {
            if (statusCode == StatusCodes.Status400BadRequest)
            {
                return Page();
                // Ще покажем BadRequest.cshtml
            }

            if (statusCode == StatusCodes.Status404NotFound)
            {
                return Page();
                // Ще покажем NotFound.cshtml
            }

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                return Page();
                // Ще покажем ServerError.cshtml
            }

            ErrorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return Page();
        }
    }
}