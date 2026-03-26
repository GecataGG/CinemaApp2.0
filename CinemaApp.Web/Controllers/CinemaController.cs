namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Core.DTOs.Cinema;
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Core.ViewModels.Cinema.WebCinema;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CinemaController : BaseController
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1. Взимаме DTOs от сървиса
            IEnumerable<CinemaAllDto> cinemaDtos =
                await this.cinemaService.GetAllCinemasOrderedByLocationAsync();

            // 2. Изграждаме ViewModel от DTOs
            CinemaIndexViewModel viewModel = new CinemaIndexViewModel
            {
                Cinemas = cinemaDtos
            };

            // 3. Връщаме ViewModel-а на View-то
            return this.View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Program([FromRoute(Name = "id")] Guid cinemaId)
        {
            // 1. Взимаме DTO от сървиса
            CinemaProgramDetailsDto? cinemaDto =
                await this.cinemaService.GetCinemaProgramByIdAsync(cinemaId);

            if (cinemaDto == null)
            {
                return this.NotFound();
            }

            // 2. Изграждаме ViewModel от DTO
            CinemaProgramViewModel viewModel = new CinemaProgramViewModel
            {
                Cinema = cinemaDto
            };

            // 3. Връщаме ViewModel-а на View-то
            return this.View(viewModel);
        }

    }
}