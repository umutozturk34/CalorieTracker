using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CalorieTracker.Models;

namespace CalorieTracker.Pages;

[Authorize]
public class PreferencesModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public PreferencesModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public string Nickname { get; set; } = string.Empty;

    [BindProperty]
    public int Age { get; set; }

    [BindProperty]
    public string Gender { get; set; } = "Male";

    [BindProperty]
    public double Height { get; set; }

    [BindProperty]
    public double CurrentWeight { get; set; }

    [BindProperty]
    public int MaxProtein { get; set; }

    [BindProperty]
    public int MaxFat { get; set; }

    [BindProperty]
    public int MaxCarb { get; set; }

    public int CalculatedCalories { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToPage("/Account/Login");

        // Load current values
        Nickname = user.Nickname;
        Age = user.Age;
        Gender = user.Gender;
        Height = user.Height;
        CurrentWeight = user.CurrentWeight;
        MaxProtein = user.MaxProtein;
        MaxFat = user.MaxFat;
        MaxCarb = user.MaxCarb;
        CalculatedCalories = user.DailyCalorieGoal;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToPage("/Account/Login");

        // Update user info
        user.Nickname = Nickname;
        user.Age = Age;
        user.Gender = Gender;
        user.Height = Height;
        user.CurrentWeight = CurrentWeight;
        user.MaxProtein = MaxProtein;
        user.MaxFat = MaxFat;
        user.MaxCarb = MaxCarb;

        // Calculate daily calorie goal from macros
        // Protein: 1g = 4 kcal
        // Fat: 1g = 9 kcal
        // Carb: 1g = 4 kcal
        user.DailyCalorieGoal = (MaxProtein * 4) + (MaxFat * 9) + (MaxCarb * 4);
        CalculatedCalories = user.DailyCalorieGoal;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            Message = "Preferences updated successfully!";
            IsSuccess = true;
        }
        else
        {
            Message = "Error updating preferences.";
            IsSuccess = false;
        }

        return Page();
    }
}
