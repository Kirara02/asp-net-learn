# 🧠 Net API Learn

A learning project built with **ASP.NET Core (.NET 9)** to create a RESTful API using **PostgreSQL**, **JWT Authentication**, and **Swagger UI**.  
The goal is to understand modern .NET API architecture — including CRUD operations, middleware, and best practices for clean configuration.

---

## 🚀 Features

✅ CRUD Product API (Create, Read, Update, Delete)  
✅ PostgreSQL with Entity Framework Core  
✅ JWT Authentication (Login & Role-based Authorization)  
✅ Automatic Database Migration on startup  
✅ Swagger UI for interactive API documentation  
✅ JSON response using `snake_case` naming policy  
✅ Clean folder structure following .NET best practices

---

## 🗂️ Project Structure

```
api-service/
├── Controllers/
│   ├── ProductsController.cs
│   └── AuthController.cs
├── Data/
│   └── AppDbContext.cs
├── Models/
│   ├── Product.cs
│   ├── User.cs
│   └── DTOs/
│       ├── ProductCreateDto.cs
│       ├── ProductReadDto.cs
│       ├── LoginRequestDto.cs
│       └── LoginResponseDto.cs
├── Program.cs
├── appsettings.json
└── Migrations/
```

---

## ⚙️ Requirements

- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- [PostgreSQL 14+](https://www.postgresql.org/download/)
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

Access the API documentation at:

```
https://localhost:5001/swagger
```

Example endpoints:

- `GET /api/products` → Get all products
- `POST /api/products` → Add a new product
- `PUT /api/products/{id}` → Update a product
- `DELETE /api/products/{id}` → Delete a product

---

## 🔐 Authentication (JWT)

Login endpoint:

```
POST /api/auth/login
```

Request:

```json
{
  "username": "admin",
  "password": "12345"
}
```

Response:

```json
{
  "token": "your.jwt.token",
  "expires_at": "2025-11-10T00:00:00Z"
}
```

Use the token in headers:

```
Authorization: Bearer your.jwt.token
```

---

## 🧹 Development Notes

- All JSON responses use `snake_case` naming (`JsonNamingPolicy.SnakeCaseLower`).
- Auto database migration runs at startup:
  ```csharp
  using (var scope = app.Services.CreateScope())
  {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.Database.Migrate();
  }
  ```

---

## 🧰 Dependencies

| Package                                       | Version | Description         |
| --------------------------------------------- | ------- | ------------------- |
| Microsoft.EntityFrameworkCore                 | 9.0.10  | ORM Core            |
| Npgsql.EntityFrameworkCore.PostgreSQL         | 9.0.4   | PostgreSQL Provider |
| Microsoft.AspNetCore.Authentication.JwtBearer | 9.0.10  | JWT Auth Middleware |
| Swashbuckle.AspNetCore                        | 9.0.6   | Swagger API Docs    |
| Microsoft.EntityFrameworkCore.Tools           | 9.0.10  | EF CLI Tools        |

---

## 👩‍💻 Author

**Kirara Bernstein**  
Mobile Developer • Flutter • Kotlin • Go • .NET Learner  
💙 Japanese Language & Hatsune Miku Enthusiast

---

## 📝 License

MIT License © 2025 Kirara Bernstein
