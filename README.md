# 🧠 Net API Learn

A learning project built with **ASP.NET Core (.NET 9)** to explore best practices for building scalable RESTful APIs.  
It integrates **PostgreSQL**, **Entity Framework Core**, **JWT Authentication**, **Serilog**, and a clean architecture approach using repository and service layers.

---

## 🚀 Features

✅ CRUD Product API  
✅ JWT Authentication & Authorization  
✅ PostgreSQL via EF Core  
✅ AutoMapper for DTO mapping  
✅ Custom Middleware for Exception Handling & Response Wrapping  
✅ Serilog Logging (Console + File)  
✅ Automatic migrations & admin seeding  
✅ Global JSON `snake_case` output  
✅ Swagger UI with JWT support

---

## 🗂️ Simplified Project Structure

```
api-service/
├── Controllers/           # API endpoints
├── Data/                  # Database context
├── Models/                # Entities & DTOs
├── Repositories/          # Data access layer
├── Services/              # Business logic layer
├── Middleware/            # Custom global middleware
├── Extensions/            # DI & configuration extensions
├── Logs/                  # Serilog log files
├── Program.cs             # Application entry point
└── appsettings.json       # Configuration
```

---

## ⚙️ Requirements

- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- [PostgreSQL 14+](https://www.postgresql.org/download/)
- [Entity Framework Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

---

## 🔧 Getting Started

### 1️⃣ Clone the repository

```bash
git clone https://github.com/kirara-bernstein/net-api-learn.git
cd net-api-learn
```

### 2️⃣ Configure Database Connection

Edit your `appsettings.json`:

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

---

## 🔐 Authentication

Use `/api/auth/login` to obtain a JWT token, then include it in all protected requests:

```
Authorization: Bearer your.jwt.token
```

Example:

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

---

## 🧩 Middleware Pipeline

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

| Package                                       | Purpose               |
| --------------------------------------------- | --------------------- |
| Microsoft.EntityFrameworkCore                 | ORM for data access   |
| Npgsql.EntityFrameworkCore.PostgreSQL         | PostgreSQL provider   |
| Microsoft.AspNetCore.Authentication.JwtBearer | JWT authentication    |
| Serilog.AspNetCore                            | Structured logging    |
| Swashbuckle.AspNetCore                        | Swagger documentation |
| AutoMapper                                    | DTO mapping           |

---

## 👩‍💻 Author

**Kirara Bernstein**  
Mobile Developer • Flutter • Kotlin • Go • .NET Learner  
💙 Japanese Language & Hatsune Miku Enthusiast

---

## 📝 License

MIT License © 2025 Kirara Bernstein
