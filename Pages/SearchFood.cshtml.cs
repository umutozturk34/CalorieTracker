using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CalorieTracker.Services;

namespace CalorieTracker.Pages;

[Authorize]
public class SearchFoodModel : PageModel
{
    private readonly FoodApiService _foodApiService;

    public SearchFoodModel(FoodApiService foodApiService)
    {
        _foodApiService = foodApiService;
    }

    [BindProperty]
    public string SearchQuery { get; set; } = string.Empty;

    [BindProperty]
    public int PageNumber { get; set; } = 1;

    public List<UsdaFoodSummary> SearchResults { get; set; } = new();
    public int TotalHits { get; set; }
    public int TotalPages { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostSearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            Message = "Please enter a food name to search.";
            IsSuccess = false;
            return Page();
        }

        try
        {
            var response = await _foodApiService.SearchFoodAsync(SearchQuery, PageNumber, 50);

            if (response?.Foods != null && response.Foods.Any())
            {
                SearchResults = response.Foods;
                TotalHits = response.TotalHits;
                TotalPages = response.TotalPages;
                Message = $"Found {response.TotalHits} results for '{SearchQuery}'";
                IsSuccess = true;
            }
            else
            {
                Message = $"No results found for '{SearchQuery}'. Try different keywords.";
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

    public async Task<IActionResult> OnPostNextPageAsync()
    {
        PageNumber++;
        return await OnPostSearchAsync();
    }

    public async Task<IActionResult> OnPostPreviousPageAsync()
    {
        if (PageNumber > 1)
        {
            PageNumber--;
        }
        return await OnPostSearchAsync();
    }
}
