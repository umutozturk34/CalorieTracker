using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CalorieTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // If user is logged in, redirect to Daily Tracker
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToPage("/DailyLog");
            }

            // Otherwise, show the landing page
            return Page();
        }
    }
}
