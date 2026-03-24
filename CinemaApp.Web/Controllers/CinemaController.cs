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
            IEnumerable<CinemaAllDto> cinemaAllDtos =
                await this.cinemaService.GetAllCinemasOrderedByLocationAsync();

            IEnumerable<CinemaIndexViewModel> cinemaIndexViewModels = cinemaAllDtos
                .Select(c => new CinemaIndexViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Location = c.Location,
                });

            return this.View(cinemaIndexViewModels);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Program([FromRoute(Name = "id")] Guid cinemaId)
        {
            CinemaProgramDetailsDto? cinemaProgramDetailsDto =
                await this.cinemaService.GetCinemaProgramByIdAsync(cinemaId);

            if (cinemaProgramDetailsDto == null)
            {
                return this.NotFound();
            }

            CinemaProgramViewModel cinemaProgramViewModel = new CinemaProgramViewModel
            {
                Id = cinemaProgramDetailsDto.Id,
                Name = cinemaProgramDetailsDto.Name,
                ProjectionMovies = cinemaProgramDetailsDto.ProjectionMovies
                    .Select(m => new CinemaProgramMoviesViewModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Director = m.Director,
                        ImageUrl = m.ImageUrl
                    })
                    .ToList()
            };

            return this.View(cinemaProgramViewModel);
        }
    }
}