using System.Text;
using System.Text.Json;

namespace CalorieTracker.Services;

public class OpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string ApiUrl = "https://api.openai.com/v1/chat/completions";

    public OpenAIService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAI API key not configured");
        
        // Set authorization header
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        
        // Set a reasonable timeout
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<string> GetFoodRecommendationAsync(int remainingCalories, string category)
    {
        try
        {
            if (remainingCalories <= 0)
            {
                return "You've reached your daily calorie goal! Consider light snacks like vegetables or fruits if needed.";
            }

            var categoryPrompt = category.ToLower() switch
            {
                "sweet" => "sweet desserts, treats, and indulgent snacks",
                "delicious" => "delicious, flavorful, and satisfying meals",
                "healthy" => "healthy, nutritious, and balanced foods",
                _ => "balanced meals"
            };

            var userPrompt = $@"I have {remainingCalories} calories remaining today. Suggest 4-5 specific {categoryPrompt} that fit within this calorie budget.

For each suggestion, provide:
- Food name
- Approximate calories per standard serving

Format your response as a clean list like this:
• Grilled Chicken Breast - 165 kcal
• Steamed Broccoli - 55 kcal
• Brown Rice (1 cup) - 216 kcal

Be specific about portions and keep total under {remainingCalories} calories.";

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "You are a helpful nutrition assistant. Provide concise, practical food recommendations."
                    },
                    new
                    {
                        role = "user",
                        content = userPrompt
                    }
                },
                temperature = 0.7,
                max_tokens = 300
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(ApiUrl, content);
            
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return "Invalid API key. Please check your OpenAI API key in settings.";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    return "API quota exceeded. Please try again later.";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return $"Bad request to OpenAI API. Details: {error}";
                }
                
                return $"API Error: {response.StatusCode}. Response: {error}";
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            
            using var jsonDoc = JsonDocument.Parse(responseContent);
            
            if (!jsonDoc.RootElement.TryGetProperty("choices", out var choices) || 
                choices.GetArrayLength() == 0)
            {
                return "No recommendations available right now. Please try again.";
            }

            var text = choices[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return text ?? "No recommendations generated. Please try again.";
        }
        catch (TaskCanceledException)
        {
            return "Request timeout. Please check your internet connection and try again.";
        }
        catch (HttpRequestException ex)
        {
            return $"Network error: Unable to connect to AI service. {ex.Message}";
        }
        catch (JsonException ex)
        {
            return $"Error parsing AI response: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Unexpected error: {ex.Message}";
        }
    }
}
