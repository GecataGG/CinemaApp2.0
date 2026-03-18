namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Core.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/Error")]
        [Route("Home/Error/{statusCode}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode = 0)
        {
            if (statusCode == StatusCodes.Status400BadRequest)
            {
                return View("BadRequest");
            }

            if (statusCode == StatusCodes.Status404NotFound)
            {
                return View("NotFound");
            }

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                return View("ServerError");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
