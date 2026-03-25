using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.App.RazorPages.Pages.Home
{
    [AllowAnonymous]
    public class PrivacyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}