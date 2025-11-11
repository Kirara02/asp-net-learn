# 🧠 Net API Learn

A learning project built with **ASP.NET Core (.NET 9)** to explore best practices for building a scalable RESTful API.  
It uses **PostgreSQL**, **Entity Framework Core**, **JWT Authentication**, **Repository & Service pattern**, and **Middleware** for structured and maintainable architecture.

---

## 🚀 Features

✅ CRUD Product API (Create, Read, Update, Delete)  
✅ Authentication & Authorization using JWT  
✅ PostgreSQL Database via Entity Framework Core  
✅ Repository & Service Layer abstraction  
✅ Custom Middleware for error handling & logging  
✅ DTO pattern with AutoMapper integration  
✅ Swagger UI documentation  
✅ Automatic migrations on startup  
✅ JSON output using `snake_case` naming policy  
✅ Clean, maintainable folder structure  

---

## 🗂️ Project Structure

```
api-service/
├── Controllers/
│   ├── ProductsController.cs
│   └── AuthController.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Models/
│   ├── Entities/
│   │   ├── Product.cs
│   │   └── User.cs
│   └── DTOs/
│       ├── Product/
│       │   ├── ProductCreateDto.cs
│       │   ├── ProductUpdateDto.cs
│       │   └── ProductReadDto.cs
│       └── Auth/
│           ├── LoginRequestDto.cs
│           └── LoginResponseDto.cs
│
├── Repositories/
│   ├── Interfaces/
│   │   ├── IProductRepository.cs
│   │   └── IUserRepository.cs
│   └── Implementations/
│       ├── ProductRepository.cs
│       └── UserRepository.cs
│
├── Services/
│   ├── Interfaces/
│   │   ├── IProductService.cs
│   │   └── IAuthService.cs
│   └── Implementations/
│       ├── ProductService.cs
│       └── AuthService.cs
│
├── Middleware/
│   ├── ExceptionMiddleware.cs
│   └── LoggingMiddleware.cs
│
├── Configurations/
│   ├── JwtSettings.cs
│   └── SwaggerConfig.cs
│
├── Extensions/
│   ├── ServiceCollectionExtensions.cs
│   ├── ApplicationBuilderExtensions.cs
│   └── AutoMapperProfile.cs
│
├── Program.cs
├── appsettings.json
└── Migrations/
```

---

## ⚙️ Requirements

- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- [PostgreSQL 14+](https://www.postgresql.org/download/)
- [Entity Framework Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- (Optional) [pgAdmin 4](https://www.pgadmin.org/download/)

---

## 🔧 Getting Started

### 1️⃣ Clone Repository
```bash
git clone https://github.com/yourusername/net-api-learn.git
cd net-api-learn
```

### 2️⃣ Configure Database Connection
Edit your `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=myapidb;Username=postgres;Password=12345"
}
```

### 3️⃣ Apply Database Migrations
```bash
dotnet ef database update --project api-service/api-service.csproj
```

### 4️⃣ Run the API
```bash
dotnet run --project api-service/api-service.csproj
```

Output:
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
```

---

## 🌐 Swagger UI

Access interactive API documentation:
```
https://localhost:5001/swagger
```

Example endpoints:
- `POST /api/auth/login` → Login with username & password  
- `GET /api/products` → Retrieve all products  
- `POST /api/products` → Create a new product (Admin only)  
- `PUT /api/products/{id}` → Update a product  
- `DELETE /api/products/{id}` → Delete a product  

---

## 🔐 Authentication (JWT)

Login endpoint:
```
POST /api/auth/login
```

Request body:
```json
{
  "username": "admin",
  "password": "12345"
}
```

Response example:
```json
{
  "token": "your.jwt.token",
  "expires_at": "2025-11-10T00:00:00Z"
}
```

Use the token in the request header:
```
Authorization: Bearer your.jwt.token
```

Protected endpoints (like `POST /api/products`) require valid tokens.

---

## 🧠 Architecture Overview

The project follows a **clean architecture** approach:

| Layer | Description |
|-------|--------------|
| **Controllers** | Handle HTTP requests and responses |
| **Services** | Contain business logic |
| **Repositories** | Manage database access using EF Core |
| **Models/DTOs** | Represent data entities and data transfer objects |
| **Middleware** | Global request/response handling (logging, exception catching) |
| **Extensions** | Dependency injection & builder helpers |
| **Configurations** | App-wide configuration settings (JWT, Swagger, etc.) |

---

## 🧹 Development Notes

- JSON naming policy set to `snake_case` globally  
- Automatic migrations executed on startup via:
  ```csharp
  using (var scope = app.Services.CreateScope())
  {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.Database.Migrate();
  }
  ```
- Passwords stored as hashed values (via `BCrypt.Net` or similar library)
- All dependency registrations handled via extension methods for clean startup configuration

---

## 🧰 Key Dependencies

| Package | Version | Purpose |
|----------|----------|----------|
| Microsoft.EntityFrameworkCore | 9.0.10 | ORM for data access |
| Npgsql.EntityFrameworkCore.PostgreSQL | 9.0.4 | PostgreSQL EF Provider |
| Microsoft.AspNetCore.Authentication.JwtBearer | 9.0.10 | JWT-based authentication |
| Swashbuckle.AspNetCore | 9.0.6 | Swagger documentation |
| Microsoft.EntityFrameworkCore.Tools | 9.0.10 | EF migration tools |
| AutoMapper.Extensions.Microsoft.DependencyInjection | 12.x | Mapping DTOs to Entities |

---

## 👩‍💻 Author

**Kirara Bernstein**  
Mobile Developer • Flutter • Kotlin • Go • .NET Learner  
💙 Japanese Language & Hatsune Miku Enthusiast  

---

## 📝 License

MIT License © 2025 Kirara Bernstein
