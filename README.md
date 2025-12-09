# 🧠 Net API Learn

A learning project built with **ASP.NET Core (.NET 9)** backend and **.NET MAUI** mobile client to explore best practices for building scalable RESTful APIs and cross-platform mobile applications. The backend integrates **PostgreSQL**, **Entity Framework Core**, **JWT Authentication**, **Serilog**, and a clean architecture approach using repository and service layers. The mobile client demonstrates API consumption, authentication, and responsive UI patterns.

---

## 🚀 Features

### Backend (api-service)

✅ CRUD Product API
✅ JWT Authentication & Authorization
✅ PostgreSQL via EF Core
✅ AutoMapper for DTO mapping
✅ Custom Middleware for Exception Handling & Response Wrapping
✅ Serilog Logging (Console + File)
✅ Automatic migrations & admin seeding
✅ Global JSON `snake_case` output
✅ Swagger UI with JWT support

### Frontend (app-client)

✅ .NET MAUI cross-platform mobile app
✅ API integration with JWT authentication
✅ Product listing and management
✅ Responsive UI with MVVM pattern
✅ Android, iOS, and desktop support

---

## 🗂️ Project Structure

```
net-api-learn/
├── api-service/           # Backend REST API
│   ├── Controllers/       # API endpoints
│   ├── Data/              # Database context
│   ├── Models/            # Entities & DTOs
│   ├── Repositories/      # Data access layer
│   ├── Services/          # Business logic layer
│   ├── Middleware/        # Custom global middleware
│   ├── Extensions/        # DI & configuration extensions
│   ├── Program.cs         # Application entry point
│   └── appsettings.json   # Configuration
│
└── app-client/            # .NET MAUI Mobile Client
    ├── App.xaml           # Application entry point
    ├── AppShell.xaml      # Navigation structure
    ├── MainPage.xaml      # Main UI page
    ├── MauiProgram.cs     # MAUI configuration
    └── ...                # Views, ViewModels, Services
```

---

## ⚙️ Requirements

### Backend Requirements

- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- [PostgreSQL 14+](https://www.postgresql.org/download/)
- [Entity Framework Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Frontend Requirements

- [.NET MAUI Workload](https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation)
- Android/iOS development environment (for mobile deployment)

---

## 🔧 Getting Started

### 1️⃣ Clone the repository

```bash
git clone https://github.com/kirara-bernstein/net-api-learn.git
cd net-api-learn
```

### 2️⃣ Configure Database Connection

Edit your `api-service/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=net_api_learn;Username=postgres;Password=12345"
}
```

### 3️⃣ Apply Migrations

```bash
dotnet ef database update --project api-service/api-service.csproj
```

> Automatically seeds a default admin user (`admin` / `admin`)

### 4️⃣ Run the API

```bash
dotnet run --project api-service/api-service.csproj
```

Access the API:

```
http://localhost:5043/swagger
```

### 5️⃣ Run the Mobile Client

```bash
dotnet run --project app-client/app-client.csproj
```

The MAUI app will launch with the appropriate platform target (Android, iOS, Windows, or macOS).

---

## 🔐 Authentication

The API uses JWT authentication. Use `/api/auth/login` to obtain a JWT token, then include it in all protected requests:

```
Authorization: Bearer your.jwt.token
```

Example response:

```json
{
  "token": "your.jwt.token",
  "user": {
    "id": 1,
    "username": "admin",
    "role": "Admin"
  }
}
```

The mobile client automatically handles authentication and token management.

---

## 🧩 Middleware Pipeline

The backend API uses the following middleware pipeline:

1️⃣ Serilog Request Logging
2️⃣ Exception Handling Middleware
3️⃣ Response Wrapper Middleware
4️⃣ Authentication & Authorization
5️⃣ Controllers Routing

All API responses follow a unified format:

```json
{
  "success": true,
  "message": "Request completed successfully.",
  "data": { ... },
  "error": null,
  "status": 200
}
```

---

## 🧰 Key Technologies

### Backend Stack

| Package                                       | Purpose               |
| --------------------------------------------- | --------------------- |
| Microsoft.EntityFrameworkCore                 | ORM for data access   |
| Npgsql.EntityFrameworkCore.PostgreSQL         | PostgreSQL provider   |
| Microsoft.AspNetCore.Authentication.JwtBearer | JWT authentication    |
| Serilog.AspNetCore                            | Structured logging    |
| Swashbuckle.AspNetCore                        | Swagger documentation |
| AutoMapper                                    | DTO mapping           |

### Frontend Stack

| Package                   | Purpose                     |
| ------------------------- | --------------------------- |
| .NET MAUI                 | Cross-platform UI framework |
| CommunityToolkit.MVVM     | MVVM pattern implementation |
| Microsoft.Extensions.Http | HTTP client integration     |

---

## 👩‍💻 Author

**Kirara Bernstein**
Mobile Developer • Flutter • Kotlin • Go • .NET Learner
💙 Japanese Language & Hatsune Miku Enthusiast

---

## 📝 License

MIT License © 2025 Kirara Bernstein
