🍎 CalorieTracker

A modern calorie and nutrition tracking application built with ASP.NET Core 8 Razor Pages.

📌 Overview

CalorieTracker helps users track daily calorie intake, macronutrients, and receive AI-powered food recommendations.
It integrates with USDA FoodData Central for accurate food data and OpenAI for personalized suggestions.

✨ Features

🥗 Daily Nutrition Tracking – Track calories, protein, fat, and carbohydrates per day

🔍 Advanced Food Search – Search over 300,000+ foods from USDA FoodData Central

🤖 AI-Powered Recommendations – Personalized food suggestions based on remaining calories & macros

👤 User Profiles – Custom daily nutrition goals per user

📊 Progress Monitoring – Visual progress bars for macros

🌙 Modern Dark UI – Responsive, dark-themed interface using Bootstrap 5

🛠️ Technologies Used

ASP.NET Core 8 – Razor Pages

Entity Framework Core – SQLite

ASP.NET Core Identity – Authentication & Authorization

Bootstrap 5 – UI framework

Bootstrap Icons – Icon library

USDA FoodData Central API – Food database

OpenAI API – AI recommendations

DotNetEnv – Environment variable management

✅ Prerequisites

.NET 8 SDK

Visual Studio 2022 or VS Code

USDA API Key

OpenAI API Key

🚀 Installation
Clone the Repository

git clone https://github.com/yourusername/CalorieTracker.git

cd CalorieTracker

Configure Environment Variables

Copy .env.example to .env and edit it:

USDA_API_KEY=your_usda_api_key_here
OPENAI_API_KEY=your_openai_api_key_here
CONNECTION_STRING=Data Source=CalorieTracker.db

Restore Packages

dotnet restore

Apply Database Migrations

dotnet ef database update

Run the Application

dotnet run

Application will be available at:
https://localhost:5001
 or http://localhost:5000

🔑 Getting API Keys
USDA FoodData Central

https://fdc.nal.usda.gov/api-key-signup.html

OpenAI API

https://platform.openai.com/api-keys

Uses gpt-4o-mini (~$0.0001 per recommendation)

🗂️ Project Structure

CalorieTracker/
├── Data/
│ └── ApplicationDbContext.cs
├── Models/
│ ├── ApplicationUser.cs
│ ├── DailyLog.cs
│ ├── FoodItem.cs
│ └── ViewModels/
├── Pages/
│ ├── Account/
│ │ ├── Login.cshtml
│ │ ├── Register.cshtml
│ │ └── Logout.cshtml
│ ├── DailyLog.cshtml
│ ├── SearchFood.cshtml
│ ├── Preferences.cshtml
│ └── Index.cshtml
├── Services/
│ ├── FoodApiService.cs
│ └── OpenAIService.cs
├── .env.example
├── .gitignore
└── Program.cs

📘 Usage

Register or log in

Set daily nutrition goals

Track meals

Get AI recommendations

Search foods

🔐 Security

API keys stored in .env

SQLite database excluded from Git

Passwords hashed with ASP.NET Core Identity

HTTPS, CSRF, and EF Core protections enabled

🌱 Environment Variables

USDA_API_KEY – Required
OPENAI_API_KEY – Required
CONNECTION_STRING – Optional

🧩 Troubleshooting

API key not configured: check .env and restart

Food search not working: verify USDA key and limits

AI not working: check OpenAI billing

🤝 Contributing

Pull requests are welcome.
Do not commit .env.

📄 License

MIT License

🙏 Acknowledgments

USDA FoodData Central
OpenAI
Bootstrap
Bootstrap Icons
DotNetEnv

📬 Contact

Open a GitHub issue for support.

Made with ❤️ using ASP.NET Core 8
