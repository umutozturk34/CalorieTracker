using System.Text;
using Newtonsoft.Json;

namespace CalorieTracker.Services;

public class FoodApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public FoodApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["USDA:ApiKey"] ?? throw new InvalidOperationException("USDA ApiKey not configured");
    }

    public async Task<UsdaFoodSearchResponse?> SearchFoodAsync(string query, int pageNumber = 1, int pageSize = 50)
    {
        try
        {
            var requestUrl = $"https://api.nal.usda.gov/fdc/v1/foods/search?api_key={_apiKey}&query={Uri.EscapeDataString(query)}&pageSize={pageSize}&pageNumber={pageNumber}";

            var response = await _httpClient.GetAsync(requestUrl);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Arama baþarýsýz. Status: {response.StatusCode}, Error: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UsdaFoodSearchResponse>(content);
        }
        catch (Exception ex)
        {
            throw new Exception($"Besin arama hatasý: {ex.Message}", ex);
        }
    }

    public async Task<UsdaFoodDetail?> GetFoodDetailAsync(int fdcId)
    {
        try
        {
            var requestUrl = $"https://api.nal.usda.gov/fdc/v1/food/{fdcId}?api_key={_apiKey}";

            var response = await _httpClient.GetAsync(requestUrl);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Besin detayý alýnamadý. Status: {response.StatusCode}, Error: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UsdaFoodDetail>(content);
        }
        catch (Exception ex)
        {
            throw new Exception($"Besin detay hatasý: {ex.Message}", ex);
        }
    }
}

// USDA API Response Models
public class UsdaFoodSearchResponse
{
    [JsonProperty("totalHits")]
    public int TotalHits { get; set; }

    [JsonProperty("currentPage")]
    public int CurrentPage { get; set; }

    [JsonProperty("totalPages")]
    public int TotalPages { get; set; }

    [JsonProperty("foods")]
    public List<UsdaFoodSummary>? Foods { get; set; }
}

public class UsdaFoodSummary
{
    [JsonProperty("fdcId")]
    public int FdcId { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("dataType")]
    public string DataType { get; set; } = string.Empty;

    [JsonProperty("brandOwner")]
    public string? BrandOwner { get; set; }

    [JsonProperty("gtinUpc")]
    public string? GtinUpc { get; set; }

    [JsonProperty("foodNutrients")]
    public List<UsdaNutrient>? FoodNutrients { get; set; }
}

public class UsdaNutrient
{
    [JsonProperty("nutrientId")]
    public int NutrientId { get; set; }

    [JsonProperty("nutrientName")]
    public string NutrientName { get; set; } = string.Empty;

    [JsonProperty("nutrientNumber")]
    public string? NutrientNumber { get; set; }

    [JsonProperty("unitName")]
    public string UnitName { get; set; } = string.Empty;

    [JsonProperty("value")]
    public double Value { get; set; }
}

public class UsdaFoodDetail
{
    [JsonProperty("fdcId")]
    public int FdcId { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("dataType")]
    public string DataType { get; set; } = string.Empty;

    [JsonProperty("brandOwner")]
    public string? BrandOwner { get; set; }

    [JsonProperty("foodNutrients")]
    public List<UsdaFoodNutrient>? FoodNutrients { get; set; }

    [JsonProperty("servingSize")]
    public double? ServingSize { get; set; }

    [JsonProperty("servingSizeUnit")]
    public string? ServingSizeUnit { get; set; }
}

public class UsdaFoodNutrient
{
    [JsonProperty("nutrient")]
    public UsdaNutrientInfo? Nutrient { get; set; }

    [JsonProperty("amount")]
    public double Amount { get; set; }
}

public class UsdaNutrientInfo
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("number")]
    public string Number { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("unitName")]
    public string UnitName { get; set; } = string.Empty;
}
