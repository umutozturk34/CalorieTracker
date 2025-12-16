using Microsoft.AspNetCore.Identity;

namespace CalorieTracker.Models;

public class ApplicationUser : IdentityUser
{
    // Personal Information
    public string Nickname { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Gender { get; set; } = "Male"; // Male, Female
    public double Height { get; set; } // in cm
    public double CurrentWeight { get; set; } // in kg
    
    // Nutrition Goals (auto-calculated from macros)
    public int DailyCalorieGoal { get; set; }
    
    // Macro Limits (in grams)
    public int MaxProtein { get; set; } // 1g = 4 kcal
    public int MaxFat { get; set; } // 1g = 9 kcal
    public int MaxCarb { get; set; } // 1g = 4 kcal
}