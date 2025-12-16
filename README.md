# CalorieTracker ??

A modern calorie and nutrition tracking application built with ASP.NET Core 8 Razor Pages.

## Features ?

- ?? **Daily Nutrition Tracking** - Track your daily calorie intake and macros
- ?? **Advanced Food Search** - Search through 300,000+ foods from USDA FoodData Central
- ?? **AI-Powered Recommendations** - Get personalized food suggestions based on your remaining calories
- ?? **User Profiles** - Customize your daily nutrition goals
- ?? **Progress Monitoring** - Track your protein, fat, and carb intake with visual progress bars
- ?? **Modern Dark UI** - Beautiful and responsive dark-themed interface

## Technologies Used ???

- **ASP.NET Core 8** - Razor Pages
- **Entity Framework Core** - SQLite database
- **ASP.NET Core Identity** - User authentication and authorization
- **Bootstrap 5** - Responsive UI framework
- **Bootstrap Icons** - Icon library
- **USDA FoodData Central API** - Food database
- **OpenAI API** - AI recommendations
- **DotNetEnv** - Environment variable management

## Prerequisites ??

- .NET 8 SDK
- Visual Studio 2022 or VS Code
- USDA API Key (free from [FoodData Central](https://fdc.nal.usda.gov/api-key-signup.html))
- OpenAI API Key (from [OpenAI Platform](https://platform.openai.com/api-keys))

## Installation ??

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/CalorieTracker.git
   cd CalorieTracker
   ```

2. **Configure API Keys with .env file** ? **RECOMMENDED**

   Copy the `.env.example` file to `.env` in the CalorieTracker directory:
   
   ```bash
   cd CalorieTracker
   cp .env.example .env
   ```

   Then edit `.env` and add your API keys:
   
   ```env
   # USDA FoodData Central API Key
   USDA_API_KEY=your_usda_api_key_here
   
   # OpenAI API Key
   OPENAI_API_KEY=your_openai_api_key_here
   
   # Database Connection (default is fine for most cases)
   CONNECTION_STRING=Data Source=CalorieTracker.db
   ```

   **?? IMPORTANT:** Never commit the `.env` file to Git! It's already in `.gitignore`.

3. **Restore packages**
   ```bash
   dotnet restore
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

   The application will be available at `https://localhost:5001` or `http://localhost:5000`

## Getting API Keys ??

### USDA FoodData Central API (FREE)
1. Go to [https://fdc.nal.usda.gov/api-key-signup.html](https://fdc.nal.usda.gov/api-key-signup.html)
2. Fill out the registration form
3. Check your email for the API key
4. Copy the key to your `.env` file

### OpenAI API (PAID)
1. Create an account at [https://platform.openai.com/](https://platform.openai.com/)
2. Add billing information (required for API access)
3. Go to [API Keys](https://platform.openai.com/api-keys)
4. Click "Create new secret key"
5. Copy the key immediately to your `.env` file (you won't see it again!)

**Note:** OpenAI API uses the `gpt-4o-mini` model which costs approximately $0.0001 per recommendation.

## Project Structure ??

```
CalorieTracker/
??? Data/
?   ??? ApplicationDbContext.cs
??? Models/
?   ??? ApplicationUser.cs
?   ??? DailyLog.cs
?   ??? FoodItem.cs
?   ??? ViewModels/
??? Pages/
?   ??? Account/
?   ?   ??? Login.cshtml
?   ?   ??? Register.cshtml
?   ?   ??? Logout.cshtml
?   ??? DailyLog.cshtml
?   ??? SearchFood.cshtml
?   ??? Preferences.cshtml
?   ??? Index.cshtml
??? Services/
?   ??? FoodApiService.cs
?   ??? OpenAIService.cs
??? .env.example          # Template for environment variables
??? .gitignore           # Protects .env from being committed
??? Program.cs
```

## Usage ??

1. **Register/Login** - Create an account or sign in
2. **Set Your Goals** - Go to Preferences to set your daily nutrition targets
3. **Track Your Meals** - Use the Daily Tracker to search and add foods
4. **Get AI Recommendations** - Click on the recommendation buttons for personalized suggestions
5. **Advanced Search** - Use Food Search for detailed food information

## Security ??

? **API Keys Protected:**
- API keys are stored in `.env` file (not committed to Git)
- `.env` is in `.gitignore` by default
- `.env.example` provides a template without actual keys

? **Database Security:**
- SQLite database is excluded from Git
- Passwords hashed using ASP.NET Core Identity

? **Application Security:**
- HTTPS enforced in production
- SQL injection protection via Entity Framework Core
- CSRF protection enabled

## Environment Variables ??

| Variable | Description | Required | Example |
|----------|-------------|----------|---------|
| `USDA_API_KEY` | USDA FoodData Central API key | Yes | `abc123...` |
| `OPENAI_API_KEY` | OpenAI API key | Yes | `sk-proj-...` |
| `CONNECTION_STRING` | Database connection string | No | `Data Source=CalorieTracker.db` |

## Troubleshooting ??

### "API key not configured" Error
- Make sure `.env` file exists in the `CalorieTracker` directory
- Check that your API keys are correctly copied (no extra spaces)
- Restart the application after changing `.env`

### Food Search Not Working
- Verify your USDA API key at [FoodData Central](https://fdc.nal.usda.gov/)
- Check if you've exceeded the rate limit (1000 requests/hour for free tier)

### AI Recommendations Not Working
- Verify your OpenAI API key at [OpenAI Platform](https://platform.openai.com/api-keys)
- Ensure billing is set up on your OpenAI account
- Check if you have available credits

## Contributing ??

Contributions are welcome! Please feel free to submit a Pull Request.

**Before contributing:**
1. Never commit `.env` file
2. Use `.env.example` to document new environment variables
3. Follow existing code style and patterns

## License ??

This project is licensed under the MIT License.

## Acknowledgments ??

- [USDA FoodData Central](https://fdc.nal.usda.gov/) - Food database
- [OpenAI](https://openai.com/) - AI recommendations
- [Bootstrap](https://getbootstrap.com/) - UI framework
- [Bootstrap Icons](https://icons.getbootstrap.com/) - Icon library
- [DotNetEnv](https://github.com/tonerdo/dotnet-env) - Environment variable management

## Contact ??

For questions or support, please open an issue on GitHub.

---

Made with ?? using ASP.NET Core 8
