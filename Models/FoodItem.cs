namespace CalorieTracker.Models;

public class FoodItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Calories { get; set; }
    public float Protein { get; set; }
    public float Fat { get; set; }
    public float Carb { get; set; }
    public string? ServingDescription { get; set; }
    public string? FatSecretId { get; set; }
    public string? BrandName { get; set; }
    
    // Navigation property
    public ICollection<DailyLog> DailyLogs { get; set; } = new List<DailyLog>();
}
