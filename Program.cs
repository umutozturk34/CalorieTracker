using CalorieTracker.Data;
using CalorieTracker.Models;
using CalorieTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using DotNetEnv;

// Load environment variables from .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Override configuration with environment variables from .env
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["USDA:ApiKey"] = Environment.GetEnvironmentVariable("USDA_API_KEY"),
    ["OpenAI:ApiKey"] = Environment.GetEnvironmentVariable("OPENAI_API_KEY"),
    ["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Data Source=CalorieTracker.db"
});

// DbContext and Identity configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // User settings
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register HttpClient and Services
builder.Services.AddHttpClient<FoodApiService>();
builder.Services.AddScoped<FoodApiService>();

builder.Services.AddHttpClient<OpenAIService>();
builder.Services.AddScoped<OpenAIService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Set culture to English (US)
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
