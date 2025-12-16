using System;

namespace CalorieTracker.Models;

public class DailyLog
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int FoodItemId { get; set; }
    public DateTime ConsumedAt { get; set; }
    
    // Grams instead of portions (100g base nutrition values)
    public decimal Grams { get; set; } = 100;

    public FoodItem FoodItem { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}