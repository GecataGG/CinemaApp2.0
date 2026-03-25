namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Core.ViewModels.Movie.WebMovie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = this.GetUserId();

            IEnumerable<AllMoviesIndexViewModel> viewModels =
                await this.movieService.GetAllMoviesOrderedByTitleAsync(userId);

            return this.View(viewModels);
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

            await this.movieService.CreateMovieAsync(formModel);

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

            MovieDetailsViewModel? viewModel =
                await this.movieService.GetMovieDetailsByIdAsync(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            MovieFormModel? formModel =
                await this.movieService.GetMovieFormModelByIdAsync(id);

            if (formModel == null)
            {
                return this.NotFound();
            }

            return this.View(formModel);
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

            await this.movieService.EditMovieAsync(id, formModel);

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            MovieDetailsViewModel? movieDetailsVm =
                await this.movieService.GetMovieDetailsByIdAsync(id);

            if (movieDetailsVm == null)
            {
                return this.NotFound();
            }

            MovieDeleteViewModel viewModel = new MovieDeleteViewModel
            {
                Id = movieDetailsVm.Id,
                Title = movieDetailsVm.Title,
                ImageUrl = movieDetailsVm.ImageUrl
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid id, MovieDeleteViewModel? deleteDetailsVm)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            await this.movieService.SoftDeleteMovieAsync(id);

            return this.RedirectToAction(nameof(Index));
        }
    }
}