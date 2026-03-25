
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
            IEnumerable<CinemaIndexViewModel> viewModels =
                await this.cinemaService.GetAllCinemasOrderedByLocationAsync();

            return this.View(viewModels);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Program([FromRoute(Name = "id")] Guid cinemaId)
        {
            CinemaProgramViewModel? viewModel =
                await this.cinemaService.GetCinemaProgramByIdAsync(cinemaId);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);

            //CinemaProgramViewModel cinemaProgramViewModel = new CinemaProgramViewModel
            //{
            //    Id = cinemaProgramDetailsDto.Id,
            //    Name = cinemaProgramDetailsDto.Name,
            //    ProjectionMovies = cinemaProgramDetailsDto.ProjectionMovies
            //        .Select(m => new CinemaProgramMoviesViewModel
            //        {
            //            Id = m.Id,
            //            Title = m.Title,
            //            Director = m.Director,
            //            ImageUrl = m.ImageUrl
            //        })
            //        .ToList()
            //};
        }
    }
}