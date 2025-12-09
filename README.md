# LibraryApi

A clean, modern REST API for managing a library's books and authors, built with .NET 10 and Entity Framework Core following Clean Architecture principles.

## 🚀 Quick Start

### Prerequisites
- .NET 10 SDK
- SQL Server (local or remote)

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/thekcr85/LibraryApi.git
   cd LibraryApi
   ```

2. **Configure the database connection**
   
   Update `LibraryApi.Api/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=LibraryDb;Trusted_Connection=true;Encrypt=false"
     }
   }
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update --project LibraryApi.Infrastructure --startup-project LibraryApi.Api
   ```

4. **Run the API**
   ```bash
   dotnet run --project LibraryApi.Api
   ```

5. **Access the API**
   - API: `https://localhost:5001/api`
   - OpenAPI: `https://localhost:5001/openapi/v1.json`

---

## 📚 API Endpoints

### Books
- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get book by ID
- `GET /api/books/isbn/{isbn}` - Get book by ISBN
- `GET /api/books/author/{authorId}` - Get books by author
- `GET /api/books/search?title={title}` - Search books by title
- `GET /api/books/year/{year}` - Get books by publication year
- `GET /api/books/recent?count={count}` - Get recent books
- `POST /api/books` - Create book
- `PUT /api/books/{id}` - Update book
- `DELETE /api/books/{id}` - Delete book

### Authors
- `GET /api/authors` - Get all authors
- `GET /api/authors/{id}` - Get author by ID
- `GET /api/authors/email/{email}` - Get author by email
- `GET /api/authors/search?name={name}` - Search authors by name
- `GET /api/authors/recent?count={count}` - Get recent authors
- `POST /api/authors` - Create author
- `PUT /api/authors/{id}` - Update author
- `DELETE /api/authors/{id}` - Delete author

---

## 🏗️ Project Structure

```
LibraryApi/
├── LibraryApi.Domain/           # Entities & business logic
├── LibraryApi.Application/      # DTOs, Services, Interfaces
├── LibraryApi.Infrastructure/   # Database, Repositories
├── LibraryApi.Api/              # Controllers, Configuration
└── LibraryApi.UnitTests/        # Unit tests
```

### Clean Architecture
- **Domain** → No external dependencies
- **Application** → Depends on Domain only
- **Infrastructure** → Implements Application interfaces
- **API** → Composition root (depends on all)

---

## 💡 Key Features

✅ **Clean Architecture** - Clear separation of concerns  
✅ **.NET 10** - Latest language features (primary constructors, records, collection expressions)  
✅ **CancellationToken Support** - Proper async/await patterns  
✅ **Entity Framework Core 10** - SQL Server integration  
✅ **OpenAPI/Swagger** - Auto-generated API documentation  
✅ **Audit Trail** - CreatedAt/UpdatedAt timestamps on all entities  
✅ **Dependency Injection** - Built-in DI container  
✅ **Validation** - Data annotations on DTOs  
✅ **Global Exception Handling** - RFC 7807 ProblemDetails error responses

---

## 🛡️ Error Handling

The API implements global exception handling following the **RFC 7807 ProblemDetails** standard for consistent error responses.

### Error Response Format

All errors return a standardized `ProblemDetails` response:

```json
{
  "type": "https://httpstatuses.com/404",
  "title": "Not found",
  "status": 404,
  "detail": "Book with ID 999 does not exist.",
  "instance": "/api/books/999"
}
```

### HTTP Status Codes

| Status | Exception | Meaning |
|--------|-----------|---------|
| 400 | `ArgumentNullException`, `ArgumentException` | Invalid request parameters |
| 404 | `KeyNotFoundException` | Resource not found |
| 409 | `InvalidOperationException` | Invalid operation state |
| 500 | Other exceptions | Internal server error |

### Development vs. Production

- **Development**: Error `Detail` includes the full exception message for debugging
- **Production**: Error `Detail` contains a generic message for security (prevents leaking internal details)

### Example Error Responses

**Not Found (404):**
```json
{
  "type": "https://httpstatuses.com/404",
  "title": "Not found",
  "status": 404,
  "detail": "Book with ID 999 does not exist.",
  "instance": "/api/books/999"
}
```

**Invalid Request (400):**
```json
{
  "type": "https://httpstatuses.com/400",
  "title": "Invalid request",
  "status": 400,
  "detail": "One or more required fields are missing.",
  "instance": "/api/books"
}
```

**Internal Server Error (500):**
```json
{
  "type": "https://httpstatuses.com/500",
  "title": "Internal server error",
  "status": 500,
  "detail": null,
  "instance": "/api/books"
}
```

---

## 📝 Example: Create a Book

```bash
curl -X POST https://localhost:5001/api/books \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Misery",
    "description": "Another terrifying experience",
    "isbn": "0132350882",
    "publishedDate": "1975-08-01",
    "authorId": 1
  }'
```

---

## 🧪 Running Tests

```bash
dotnet test LibraryApi.UnitTests
```

---

## 📦 Technologies

- **Runtime**: .NET 10
- **Language**: C# 14
- **Database**: SQL Server + Entity Framework Core 10
- **API**: ASP.NET Core Web API
- **Validation**: Data Annotations
- **Documentation**: OpenAPI/Scalar

---

## 🔗 Resources

- [.NET 10 Docs](https://learn.microsoft.com/en-us/dotnet/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)

---

## 📄 License

This project is open source and available under the MIT License.

---

## 👨‍💻 Contributing

Feel free to fork and submit pull requests!
