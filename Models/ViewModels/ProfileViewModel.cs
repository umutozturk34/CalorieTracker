using System.ComponentModel.DataAnnotations;

namespace CalorieTracker.Models.ViewModels;

public class ProfileViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(50, 300, ErrorMessage = "Height must be between 50 and 300 cm.")]
    public double Height { get; set; }

    [Required]
    [Range(20, 500, ErrorMessage = "Weight must be between 20 and 500 kg.")]
    public double CurrentWeight { get; set; }

    [Required]
    [Range(1000, 10000, ErrorMessage = "Daily calorie goal must be between 1000 and 10000.")]
    public int DailyCalorieGoal { get; set; }
}