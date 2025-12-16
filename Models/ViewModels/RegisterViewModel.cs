using System.ComponentModel.DataAnnotations;

namespace CalorieTracker.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    [Range(50, 300, ErrorMessage = "Height must be between 50 and 300 cm.")]
    public double Height { get; set; }

    [Required]
    [Range(20, 500, ErrorMessage = "Weight must be between 20 and 500 kg.")]
    public double CurrentWeight { get; set; }
}