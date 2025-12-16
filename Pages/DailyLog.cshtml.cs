using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CalorieTracker.Data;
using CalorieTracker.Models;
using CalorieTracker.Services;
using System.Security.Claims;

namespace CalorieTracker.Pages;

[Authorize]
public class DailyLogModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly FoodApiService _foodApiService;
    private readonly OpenAIService _openAIService;

    public DailyLogModel(ApplicationDbContext context, FoodApiService foodApiService, OpenAIService openAIService)
    {
        _context = context;
        _foodApiService = foodApiService;
        _openAIService = openAIService;
    }

    [BindProperty]
    public string SearchQuery { get; set; } = string.Empty;

    [BindProperty]
    public int SelectedFdcId { get; set; }

    [BindProperty]
    public decimal Grams { get; set; } = 100;

    [BindProperty(SupportsGet = true)]
    public DateTime SelectedDate { get; set; } = DateTime.Today;

    public List<DailyLog> TodayLogs { get; set; } = new();
    public List<UsdaFoodSummary> SearchResults { get; set; } = new();
    
    // Summary properties
    public decimal TotalCalories { get; set; }
    public decimal TotalProtein { get; set; }
    public decimal TotalFat { get; set; }
    public decimal TotalCarb { get; set; }
    public int RemainingCalories { get; set; }

    // User goals
    public ApplicationUser CurrentUser { get; set; } = null!;

    public string? Message { get; set; }
    public bool IsSuccess { get; set; }

    // AI Recommendations
    public string? AiRecommendation { get; set; }

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId == null)
            return;

        // Get current user with goals
        CurrentUser = await _context.Users.FindAsync(userId) ?? new ApplicationUser();

        // Get today's logs
        TodayLogs = await _context.DailyLogs
            .Include(d => d.FoodItem)
            .Where(d => d.UserId == userId && d.ConsumedAt.Date == SelectedDate.Date)
            .OrderByDescending(d => d.ConsumedAt)
            .ToListAsync();

        // Calculate totals based on grams (nutrition values are per 100g)
        foreach (var log in TodayLogs)
        {
            var multiplier = log.Grams / 100m;
            TotalCalories += log.FoodItem.Calories * multiplier;
            TotalProtein += (decimal)log.FoodItem.Protein * multiplier;
            TotalFat += (decimal)log.FoodItem.Fat * multiplier;
            TotalCarb += (decimal)log.FoodItem.Carb * multiplier;
        }

        RemainingCalories = CurrentUser.DailyCalorieGoal - (int)TotalCalories;
    }

    public async Task<IActionResult> OnPostSearchFoodAsync()
    {
        await OnGetAsync();

        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            Message = "Please enter a food name.";
            IsSuccess = false;
            return Page();
        }

        try
        {
            var response = await _foodApiService.SearchFoodAsync(SearchQuery, 1, 20);
            
            if (response?.Foods != null && response.Foods.Any())
            {
                SearchResults = response.Foods;
                Message = $"{response.TotalHits} results found.";
                IsSuccess = true;
            }
            else
            {
                Message = "No results found. Try different keywords.";
                IsSuccess = false;
            }
        }
        catch (Exception ex)
        {
            Message = $"Search error: {ex.Message}";
            IsSuccess = false;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostGetRecommendationAsync(string category)
    {
        await OnGetAsync();

        if (RemainingCalories <= 0)
        {
            AiRecommendation = "You've reached your daily calorie goal! Great job!";
            return Page();
        }

        try
        {
            AiRecommendation = await _openAIService.GetFoodRecommendationAsync(RemainingCalories, category);
        }
        catch (Exception ex)
        {
            AiRecommendation = $"Error getting recommendations: {ex.Message}";
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddFoodAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId == null)
            return RedirectToPage("/Account/Login");

        if (SelectedFdcId == 0 || Grams <= 0)
        {
            ModelState.AddModelError("", "Select a valid food and enter grams.");
            await OnGetAsync();
            return Page();
        }

        try
        {
            // Get detailed food information from USDA API
            var foodDetail = await _foodApiService.GetFoodDetailAsync(SelectedFdcId);
            
            if (foodDetail == null)
            {
                Message = "Could not retrieve food details.";
                IsSuccess = false;
                await OnGetAsync();
                return Page();
            }

            // Check if food already exists in database
            var existingFood = await _context.FoodItems
                .FirstOrDefaultAsync(f => f.FatSecretId == SelectedFdcId.ToString());

            FoodItem foodItem;

            if (existingFood != null)
            {
                foodItem = existingFood;
            }
            else
            {
                // Extract nutrition values (per 100g from USDA)
                var calories = GetNutrientValue(foodDetail.FoodNutrients, "Energy", "208");
                var protein = GetNutrientValue(foodDetail.FoodNutrients, "Protein", "203");
                var fat = GetNutrientValue(foodDetail.FoodNutrients, "Total lipid (fat)", "204");
                var carb = GetNutrientValue(foodDetail.FoodNutrients, "Carbohydrate, by difference", "205");

                var servingDesc = foodDetail.ServingSize.HasValue 
                    ? $"{foodDetail.ServingSize} {foodDetail.ServingSizeUnit}" 
                    : "100g";

                // Create new FoodItem (values are per 100g)
                foodItem = new FoodItem
                {
                    Name = foodDetail.Description,
                    Calories = (int)calories,
                    Protein = (float)protein,
                    Fat = (float)fat,
                    Carb = (float)carb,
                    FatSecretId = SelectedFdcId.ToString(),
                    ServingDescription = servingDesc,
                    BrandName = foodDetail.BrandOwner
                };

                _context.FoodItems.Add(foodItem);
                await _context.SaveChangesAsync();
            }

            // Add to daily log with selected date + current time
            var dailyLog = new DailyLog
            {
                UserId = userId,
                FoodItemId = foodItem.Id,
                Grams = Grams,
                ConsumedAt = SelectedDate.Date.Add(DateTime.Now.TimeOfDay)
            };

            _context.DailyLogs.Add(dailyLog);
            await _context.SaveChangesAsync();

            // Show appropriate message based on selected date
            if (SelectedDate.Date < DateTime.Today)
            {
                var daysAgo = (DateTime.Today - SelectedDate.Date).Days;
                Message = $"?? '{foodItem.Name}' ({Grams}g) added to {SelectedDate:MMMM dd, yyyy} ({daysAgo} day{(daysAgo > 1 ? "s" : "")} ago)";
                IsSuccess = true;
            }
            else if (SelectedDate.Date > DateTime.Today)
            {
                var daysAhead = (SelectedDate.Date - DateTime.Today).Days;
                Message = $"?? '{foodItem.Name}' ({Grams}g) added to {SelectedDate:MMMM dd, yyyy} ({daysAhead} day{(daysAhead > 1 ? "s" : "")} ahead)";
                IsSuccess = true;
            }
            else
            {
                Message = $"? '{foodItem.Name}' ({Grams}g) added to today!";
                IsSuccess = true;
            }

            return RedirectToPage(new { selectedDate = SelectedDate });
        }
        catch (Exception ex)
        {
            Message = $"Error adding food: {ex.Message}";
            IsSuccess = false;
            await OnGetAsync();
            return Page();
        }
    }

    public async Task<IActionResult> OnPostDeleteLogAsync(int logId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId == null)
            return RedirectToPage("/Account/Login");

        var log = await _context.DailyLogs
            .FirstOrDefaultAsync(d => d.Id == logId && d.UserId == userId);

        if (log != null)
        {
            _context.DailyLogs.Remove(log);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage(new { selectedDate = SelectedDate });
    }

    private double GetNutrientValue(List<UsdaFoodNutrient>? nutrients, string nutrientName, string nutrientNumber)
    {
        if (nutrients == null) return 0;

        var nutrient = nutrients.FirstOrDefault(n => 
            n.Nutrient?.Name?.Contains(nutrientName, StringComparison.OrdinalIgnoreCase) == true ||
            n.Nutrient?.Number == nutrientNumber);

        return nutrient?.Amount ?? 0;
    }
}
