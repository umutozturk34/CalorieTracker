using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CalorieTracker.Models.ViewModels;
using CalorieTracker.Models;

namespace CalorieTracker.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public RegisterViewModel Input { get; set; } = new();

    public string? ErrorMessage { get; set; }
    public List<string> Errors { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Collect all model state errors
            Errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return Page();
        }

        try
        {
            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(Input.Email);
            if (existingUser != null)
            {
                ErrorMessage = "An account with this email address already exists. Please sign in or use a different email.";
                return Page();
            }

            // Create new user
            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                Height = Input.Height,
                CurrentWeight = Input.CurrentWeight
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Preferences", new { firstTime = true });
            }

            // Collect Identity errors
            Errors = result.Errors.Select(e => FormatIdentityError(e)).ToList();
            ErrorMessage = "Registration failed. Please check the errors below.";
            return Page();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An unexpected error occurred during registration: {ex.Message}";
            return Page();
        }
    }

    private string FormatIdentityError(IdentityError error)
    {
        return error.Code switch
        {
            "DuplicateUserName" => "This email is already registered.",
            "InvalidEmail" => "Please enter a valid email address.",
            "PasswordTooShort" => $"Password must be at least {_userManager.Options.Password.RequiredLength} characters long.",
            "PasswordRequiresNonAlphanumeric" => "Password must contain at least one special character (!@#$%^&*).",
            "PasswordRequiresDigit" => "Password must contain at least one number (0-9).",
            "PasswordRequiresLower" => "Password must contain at least one lowercase letter (a-z).",
            "PasswordRequiresUpper" => "Password must contain at least one uppercase letter (A-Z).",
            _ => error.Description
        };
    }
}