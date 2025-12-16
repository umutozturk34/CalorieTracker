using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CalorieTracker.Models.ViewModels;
using CalorieTracker.Models;

namespace CalorieTracker.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty]
    public LoginViewModel Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            // Check if user exists
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ErrorMessage = "No account found with this email address. Please check your email or create a new account.";
                return Page();
            }

            // Attempt to sign in
            var result = await _signInManager.PasswordSignInAsync(
                Input.Email, 
                Input.Password, 
                Input.RememberMe, 
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToPage("/DailyLog");
            }

            if (result.IsLockedOut)
            {
                ErrorMessage = "Your account has been locked due to multiple failed login attempts. Please try again later.";
                return Page();
            }

            if (result.IsNotAllowed)
            {
                ErrorMessage = "You are not allowed to sign in. Please confirm your email address.";
                return Page();
            }

            // Generic password error
            ErrorMessage = "Invalid password. Please check your password and try again.";
            return Page();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An unexpected error occurred: {ex.Message}";
            return Page();
        }
    }
}