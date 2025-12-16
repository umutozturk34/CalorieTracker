# CalorieTracker ??
# 🍎 CalorieTracker

A modern calorie and nutrition tracking application built with ASP.NET Core 8 Razor Pages.
A modern calorie and nutrition tracking application built with **ASP.NET Core 8 Razor Pages**.

## Features ?
---

- ?? **Daily Nutrition Tracking** - Track your daily calorie intake and macros
- ?? **Advanced Food Search** - Search through 300,000+ foods from USDA FoodData Central
- ?? **AI-Powered Recommendations** - Get personalized food suggestions based on your remaining calories
- ?? **User Profiles** - Customize your daily nutrition goals
- ?? **Progress Monitoring** - Track your protein, fat, and carb intake with visual progress bars
- ?? **Modern Dark UI** - Beautiful and responsive dark-themed interface
## 📌 Overview

## Technologies Used ???
**CalorieTracker** helps users monitor daily calorie intake, macronutrients, and receive AI-powered food recommendations. It integrates with:
- **USDA FoodData Central** for accurate food data
- **OpenAI GPT-4** for personalized meal suggestions

- **ASP.NET Core 8** - Razor Pages
- **Entity Framework Core** - SQLite database
- **ASP.NET Core Identity** - User authentication and authorization
- **Bootstrap 5** - Responsive UI framework
- **Bootstrap Icons** - Icon library
- **USDA FoodData Central API** - Food database
- **OpenAI API** - AI recommendations
- **DotNetEnv** - Environment variable management
---

## Prerequisites ??
## ✨ Features

- .NET 8 SDK
- Visual Studio 2022 or VS Code
- USDA API Key (free from [FoodData Central](https://fdc.nal.usda.gov/api-key-signup.html))
- OpenAI API Key (from [OpenAI Platform](https://platform.openai.com/api-keys))
- 🥗 **Daily Nutrition Tracking** – Monitor calories, protein, fat, and carbohydrates
- 🔍 **Advanced Food Search** – Access 300,000+ foods from USDA FoodData Central
- 🤖 **AI-Powered Recommendations** – Get personalized food suggestions based on remaining calories
- 👤 **User Profiles** – Set custom daily nutrition goals
- 📊 **Progress Monitoring** – Visual progress bars for macros
- 🌙 **Modern Dark UI** – Responsive dark-themed interface with Bootstrap 5
- 📅 **Historical Tracking** – View and edit past days' food entries

## Installation ??
---

1. **Clone the repository**
## 🛠️ Technologies Used

| Technology | Purpose |
|------------|---------|
| **ASP.NET Core 8** | Razor Pages framework |
| **Entity Framework Core** | SQLite database ORM |
| **ASP.NET Core Identity** | Authentication & Authorization |
| **Bootstrap 5** | Responsive UI framework |
| **Bootstrap Icons** | Icon library |
| **USDA FoodData Central API** | Food database (300k+ items) |
| **OpenAI API** | AI-powered recommendations |
| **DotNetEnv** | Environment variable management |

---

## ✅ Prerequisites

Before you begin, ensure you have:

- ✔️ **.NET 8 SDK** installed
- ✔️ **Visual Studio 2022** or **VS Code**
- ✔️ **USDA API Key** (free) - [Get it here](https://fdc.nal.usda.gov/api-key-signup.html)
- ✔️ **OpenAI API Key** (paid) - [Get it here](https://platform.openai.com/api-keys)

---

## 🚀 Installation

### 1️⃣ Clone the Repository

```bash
   git clone https://github.com/yourusername/CalorieTracker.git
git clone https://github.com/umutozturk34/CalorieTracker.git
cd CalorieTracker
```

2. **Configure API Keys with .env file** ? **RECOMMENDED**
### 2️⃣ Configure Environment Variables

   Copy the `.env.example` file to `.env` in the CalorieTracker directory:
Copy `.env.example` to `.env`:

```bash
   cd CalorieTracker
cp .env.example .env
```

   Then edit `.env` and add your API keys:
Edit `.env` with your API keys:

```env
   # USDA FoodData Central API Key
# USDA FoodData Central API Key (FREE)
USDA_API_KEY=your_usda_api_key_here

   # OpenAI API Key
# OpenAI API Key (PAID - ~$0.0001 per recommendation)
OPENAI_API_KEY=your_openai_api_key_here

   # Database Connection (default is fine for most cases)
# Database Connection (default is fine)
CONNECTION_STRING=Data Source=CalorieTracker.db
```

   **?? IMPORTANT:** Never commit the `.env` file to Git! It's already in `.gitignore`.
> ⚠️ **IMPORTANT:** Never commit the `.env` file! It's already in `.gitignore`.

3. **Restore packages**
### 3️⃣ Restore NuGet Packages

```bash
dotnet restore
```

4. **Apply Database Migrations**
### 4️⃣ Apply Database Migrations

```bash
dotnet ef database update
```

5. **Run the Application**
### 5️⃣ Run the Application

```bash
dotnet run
```

   The application will be available at `https://localhost:5001` or `http://localhost:5000`
The application will be available at:
- 🌐 **HTTPS:** `https://localhost:5001`
- 🌐 **HTTP:** `http://localhost:5000`

## Getting API Keys ??
---

### USDA FoodData Central API (FREE)
1. Go to [https://fdc.nal.usda.gov/api-key-signup.html](https://fdc.nal.usda.gov/api-key-signup.html)
## 🔑 Getting API Keys

### 🌾 USDA FoodData Central API (FREE)

1. Visit: [https://fdc.nal.usda.gov/api-key-signup.html](https://fdc.nal.usda.gov/api-key-signup.html)
2. Fill out the registration form
3. Check your email for the API key
4. Copy the key to your `.env` file
4. Copy it to your `.env` file

### OpenAI API (PAID)
1. Create an account at [https://platform.openai.com/](https://platform.openai.com/)
2. Add billing information (required for API access)
3. Go to [API Keys](https://platform.openai.com/api-keys)
4. Click "Create new secret key"
5. Copy the key immediately to your `.env` file (you won't see it again!)
**Rate Limit:** 1,000 requests/hour (free tier)

**Note:** OpenAI API uses the `gpt-4o-mini` model which costs approximately $0.0001 per recommendation.
### 🤖 OpenAI API (PAID)

## Project Structure ??
1. Create account: [https://platform.openai.com/](https://platform.openai.com/)
2. Add billing information (required)
3. Generate API key: [https://platform.openai.com/api-keys](https://platform.openai.com/api-keys)
4. Copy it immediately to `.env` (you won't see it again!)

**Model Used:** `gpt-4o-mini` (~$0.0001 per recommendation)

---

## 🗂️ Project Structure

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
├── 📁 Data/
│   └── ApplicationDbContext.cs
├── 📁 Models/
│   ├── ApplicationUser.cs
│   ├── DailyLog.cs
│   ├── FoodItem.cs
│   └── ViewModels/
├── 📁 Pages/
│   ├── 📁 Account/
│   │   ├── Login.cshtml
│   │   ├── Register.cshtml
│   │   └── Logout.cshtml
│   ├── DailyLog.cshtml
│   ├── SearchFood.cshtml
│   ├── Preferences.cshtml
│   └── Index.cshtml
├── 📁 Services/
│   ├── FoodApiService.cs
│   └── OpenAIService.cs
├── 📁 Migrations/
├── .env.example          # 🔒 Template for secrets
├── .gitignore            # 🛡️ Protects sensitive files
└── Program.cs
```

## Usage ??
---

## 📘 Usage Guide

### 1️⃣ Register / Login
Create an account or sign in with existing credentials.

### 2️⃣ Set Daily Goals
Go to **Preferences** page and set your:
- Daily calorie goal
- Maximum protein/fat/carbs intake
- Personal information (age, gender, height, weight)

### 3️⃣ Track Meals
On **Daily Log** page:
- Search for foods using USDA database
- Enter grams consumed
- Add to your daily log
- View nutritional breakdown

### 4️⃣ Get AI Recommendations
Click recommendation buttons:
- 🍰 **Sweet Treats**
- 😋 **Delicious Meals**
- 🥗 **Healthy Options**

AI suggests foods based on your remaining calories!

### 5️⃣ View History
Use the date picker to view/edit past days' food entries.

---

## 🔐 Security Features

| Feature | Implementation |
|---------|----------------|
| **API Key Protection** | `.env` file excluded from Git via `.gitignore` |
| **Database Security** | `*.db` files excluded from version control |
| **Password Security** | Hashed using ASP.NET Core Identity (PBKDF2) |
| **HTTPS** | Enforced in production |
| **SQL Injection** | Protected via Entity Framework Core |
| **CSRF Protection** | Enabled by default in Razor Pages |

> 🔒 **Never commit `.env` or `*.db` files to version control!**

---

## 🌱 Environment Variables

1. **Register/Login** - Create an account or sign in
2. **Set Your Goals** - Go to Preferences to set your daily nutrition targets
3. **Track Your Meals** - Use the Daily Tracker to search and add foods
4. **Get AI Recommendations** - Click on the recommendation buttons for personalized suggestions
5. **Advanced Search** - Use Food Search for detailed food information
| Variable | Required | Description | Example |
|----------|----------|-------------|---------|
| `USDA_API_KEY` | ✅ Yes | USDA FoodData Central API key | `abc123xyz...` |
| `OPENAI_API_KEY` | ✅ Yes | OpenAI API key | `sk-proj-...` |
| `CONNECTION_STRING` | ❌ No | Database connection string | `Data Source=CalorieTracker.db` |

## Security ??
---

? **API Keys Protected:**
- API keys are stored in `.env` file (not committed to Git)
- `.env` is in `.gitignore` by default
- `.env.example` provides a template without actual keys
## 🧩 Troubleshooting

? **Database Security:**
- SQLite database is excluded from Git
- Passwords hashed using ASP.NET Core Identity
### ❌ "API key not configured" Error
- ✔️ Ensure `.env` file exists in the `CalorieTracker` directory
- ✔️ Check for extra spaces in API keys
- ✔️ Restart the application after editing `.env`

? **Application Security:**
- HTTPS enforced in production
- SQL injection protection via Entity Framework Core
- CSRF protection enabled
### 🔍 Food Search Not Working
- ✔️ Verify USDA API key at [FoodData Central](https://fdc.nal.usda.gov/)
- ✔️ Check rate limit (1000 requests/hour for free tier)
- ✔️ Ensure internet connection is active

## Environment Variables ??
### 🤖 AI Recommendations Not Working
- ✔️ Verify OpenAI API key at [OpenAI Platform](https://platform.openai.com/api-keys)
- ✔️ Ensure billing is set up on OpenAI account
- ✔️ Check available credits/usage limits

| Variable | Description | Required | Example |
|----------|-------------|----------|---------|
| `USDA_API_KEY` | USDA FoodData Central API key | Yes | `abc123...` |
| `OPENAI_API_KEY` | OpenAI API key | Yes | `sk-proj-...` |
| `CONNECTION_STRING` | Database connection string | No | `Data Source=CalorieTracker.db` |
### 🗄️ Database Issues
```bash
# Reset database
dotnet ef database drop
dotnet ef database update
```

## Troubleshooting ??
---

### "API key not configured" Error
- Make sure `.env` file exists in the `CalorieTracker` directory
- Check that your API keys are correctly copied (no extra spaces)
- Restart the application after changing `.env`
## 🤝 Contributing

### Food Search Not Working
- Verify your USDA API key at [FoodData Central](https://fdc.nal.usda.gov/)
- Check if you've exceeded the rate limit (1000 requests/hour for free tier)
Contributions are welcome! Please follow these guidelines:

### AI Recommendations Not Working
- Verify your OpenAI API key at [OpenAI Platform](https://platform.openai.com/api-keys)
- Ensure billing is set up on your OpenAI account
- Check if you have available credits
1. 🍴 Fork the repository
2. 🌿 Create a feature branch (`git checkout -b feature/amazing-feature`)
3. 💾 Commit changes (`git commit -m 'Add amazing feature'`)
4. 📤 Push to branch (`git push origin feature/amazing-feature`)
5. 🎉 Open a Pull Request

## Contributing ??
### ⚠️ Before Contributing:
- ❌ **Never commit `.env` file**
- ✅ Use `.env.example` for documenting new variables
- ✅ Follow existing code style and patterns
- ✅ Test your changes thoroughly

Contributions are welcome! Please feel free to submit a Pull Request.
---

**Before contributing:**
1. Never commit `.env` file
2. Use `.env.example` to document new environment variables
3. Follow existing code style and patterns
## 📄 License

## License ??
This project is licensed under the **MIT License**.

This project is licensed under the MIT License.
---

## Acknowledgments ??
## 🙏 Acknowledgments

- [USDA FoodData Central](https://fdc.nal.usda.gov/) - Food database
- [OpenAI](https://openai.com/) - AI recommendations
- [USDA FoodData Central](https://fdc.nal.usda.gov/) - Comprehensive food database
- [OpenAI](https://openai.com/) - AI-powered recommendations
- [Bootstrap](https://getbootstrap.com/) - UI framework
- [Bootstrap Icons](https://icons.getbootstrap.com/) - Icon library
- [DotNetEnv](https://github.com/tonerdo/dotnet-env) - Environment variable management

## Contact ??
---

## 📬 Contact & Support

For questions or support, please open an issue on GitHub.
- 🐛 **Bug Reports:** Open an issue on [GitHub Issues](https://github.com/umutozturk34/CalorieTracker/issues)
- 💡 **Feature Requests:** Submit via GitHub Issues
- 📧 **Questions:** Open a discussion

---

Made with ?? using ASP.NET Core 8
<div align="center">

**Made with ❤️ using ASP.NET Core 8**

⭐ **Star this repo if you find it helpful!** ⭐

[Report Bug](https://github.com/umutozturk34/CalorieTracker/issues) · [Request Feature](https://github.com/umutozturk34/CalorieTracker/issues)

</div>
