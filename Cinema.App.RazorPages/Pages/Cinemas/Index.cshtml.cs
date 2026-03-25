using CinemaApp.Core.DTOs.Cinema;
using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Cinema.WebCinema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Cinemas
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ICinemaService _cinemaService;

        public IndexModel(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        public CinemaIndexViewModel ViewModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Взимаме DTOs от сървиса
            IEnumerable<CinemaAllDto> cinemaDtos =
                await _cinemaService.GetAllCinemasOrderedByLocationAsync();

            // 2. Изграждаме ViewModel от DTOs
            ViewModel = new CinemaIndexViewModel
            {
                Cinemas = cinemaDtos
            };

            // 3. Връщаме страницата
            return Page();
        }
    }
}