namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Core.DTOs.Movie;
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Core.ViewModels.Movie.WebMovie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly ILogger<MovieController> logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            this.movieService = movieService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = this.GetUserId();

            IEnumerable<MovieAllDto> movieAllDtos =
                await this.movieService.GetAllMoviesOrderedByTitleAsync(userId);

            IEnumerable<AllMoviesIndexViewModel> allMoviesIndexVms = movieAllDtos
                .Select(m => new AllMoviesIndexViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Director = m.Director,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate.ToString("yyyy-MM-dd"),
                    ImageUrl = m.ImageUrl,
                    IsInUserWatchlist = m.IsInUserWatchlist
                })
                .ToList();

            return this.View(allMoviesIndexVms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormModel formModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(formModel);
            }

            try
            {
                MovieDetailsDto movieDetailsDto = new MovieDetailsDto
                {
                    Title = formModel.Title,
                    Description = formModel.Description,
                    Director = formModel.Director,
                    Genre = formModel.Genre,
                    Duration = formModel.Duration,
                    ReleaseDate = formModel.ReleaseDate,
                    ImageUrl = formModel.ImageUrl
                };

                await this.movieService.CreateMovieAsync(movieDetailsDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while creating movie");
                this.TempData["Error"] = "Error while creating movie.";

                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            MovieDetailsDto? movieDetailsDto =
                await this.movieService.GetMovieDetailsByIdAsync(id);

            if (movieDetailsDto == null)
            {
                return this.NotFound();
            }

            MovieDetailsViewModel movieDetailsVm = new MovieDetailsViewModel
            {
                Id = movieDetailsDto.Id,
                Title = movieDetailsDto.Title,
                Description = movieDetailsDto.Description,
                Director = movieDetailsDto.Director,
                Genre = movieDetailsDto.Genre,
                Duration = movieDetailsDto.Duration,
                ReleaseDate = movieDetailsDto.ReleaseDate.ToString("yyyy-MM-dd"),
                ImageUrl = movieDetailsDto.ImageUrl,
                IsInUserWatchlist = movieDetailsDto.IsInUserWatchlist
            };

            return this.View(movieDetailsVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            MovieDetailsDto? movieDetailsDto =
                await this.movieService.GetMovieFormModelByIdAsync(id);

            if (movieDetailsDto == null)
            {
                return this.NotFound();
            }

            MovieFormModel movieFormModel = new MovieFormModel
            {
                Title = movieDetailsDto.Title,
                Description = movieDetailsDto.Description,
                Director = movieDetailsDto.Director,
                Genre = movieDetailsDto.Genre,
                Duration = movieDetailsDto.Duration,
                ReleaseDate = movieDetailsDto.ReleaseDate,
                ImageUrl = movieDetailsDto.ImageUrl
            };

            return this.View(movieFormModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid id, MovieFormModel formModel)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(formModel);
            }

            try
            {
                MovieDetailsDto movieDetailsDto = new MovieDetailsDto
                {
                    Title = formModel.Title,
                    Description = formModel.Description,
                    Director = formModel.Director,
                    Genre = formModel.Genre,
                    Duration = formModel.Duration,
                    ReleaseDate = formModel.ReleaseDate,
                    ImageUrl = formModel.ImageUrl
                };

                await this.movieService.EditMovieAsync(id, movieDetailsDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while editing movie");
                this.TempData["Error"] = "Error while editing movie.";

                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            MovieDetailsDto? movieDetailsDto =
                await this.movieService.GetMovieDetailsByIdAsync(id);

            if (movieDetailsDto == null)
            {
                return this.NotFound();
            }

            MovieDeleteViewModel movieDeleteVm = new MovieDeleteViewModel
            {
                Id = movieDetailsDto.Id,
                Title = movieDetailsDto.Title,
                ImageUrl = movieDetailsDto.ImageUrl
            };

            return this.View(movieDeleteVm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid id, MovieDeleteViewModel? deleteDetailsVm)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            try
            {
                await this.movieService.SoftDeleteMovieAsync(id);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while deleting movie");
                this.TempData["Error"] = "Error while deleting movie.";

                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }
    }
}
