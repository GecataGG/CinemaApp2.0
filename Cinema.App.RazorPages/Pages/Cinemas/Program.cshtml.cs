using CinemaApp.Core.DTOs.Cinema;
using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Cinema.WebCinema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Cinemas
{
    [AllowAnonymous]
    public class ProgramModel : PageModel
    {
        private readonly ICinemaService _cinemaService;

        public ProgramModel(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        public CinemaProgramViewModel ViewModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            // 1. Взимаме DTO от сървиса
            CinemaProgramDetailsDto? cinemaDto =
                await _cinemaService.GetCinemaProgramByIdAsync(id);

            if (cinemaDto == null)
            {
                return NotFound();
            }

            // 2. Изграждаме ViewModel от DTO
            ViewModel = new CinemaProgramViewModel
            {
                Cinema = cinemaDto
            };

            // 3. Връщаме страницата
            return Page();
        }
    }
}