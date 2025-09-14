# NetCrud - .NET CRUD API with MySQL

A well-structured .NET 8 Web API project implementing CRUD operations with MySQL database, following Clean Architecture principles.

## 🏗️ Architecture

This project follows Clean Architecture (Onion Architecture) with the following layers:

```
NetCrud/
├── src/
│   ├── NetCrud.API/           # Presentation Layer (Controllers, DTOs)
│   ├── NetCrud.Application/   # Application Layer (Services, Interfaces)
│   ├── NetCrud.Domain/        # Domain Layer (Entities, Interfaces)
│   └── NetCrud.Infrastructure/# Infrastructure Layer (Data Access, External Services)
└── tests/                     # Test Projects
```

## 🚀 Features

- **Clean Architecture**: Separation of concerns with proper dependency inversion
- **Entity Framework Core**: ORM with MySQL database support
- **Repository Pattern**: Generic repository with Unit of Work pattern
- **Dependency Injection**: Proper service registration and lifetime management
- **Swagger Documentation**: Interactive API documentation
- **CORS Support**: Cross-origin resource sharing enabled
- **Soft Delete**: Logical deletion with query filters
- **Validation**: Data annotations and model validation
- **Error Handling**: Comprehensive error handling and logging
- **MySQL Integration**: Pomelo.EntityFrameworkCore.MySql provider

## 🛠️ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) (8.0 or later)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/)

## 📦 Database Setup

1. **Install MySQL Server** (if not already installed)
2. **Create Database**:
   ```sql
   CREATE DATABASE NetCrudDB;
   ```
3. **Update Connection String** in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=NetCrudDB;Uid=root;Pwd=breitling;Port=3306;"
     }
   }
   ```

## 🚀 Getting Started

1. **Clone the repository**:

   ```bash
   git clone <repository-url>
   cd net_crud
   ```

2. **Restore packages**:

   ```bash
   dotnet restore
   ```

3. **Configure database connection**:

   ```bash
   cd src/NetCrud.API/NetCrud.API
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=NetCrudDB;Uid=root;Pwd=YOUR_PASSWORD;Port=3306;"
   ```

4. **Update database**:

   ```bash
   dotnet ef database update
   ```

5. **Run the application**:

   ```bash
   # Option 1: Quick start (faster)
   ./start.sh

   # Option 2: Development with hot reload
   ./dev.sh

   # Option 3: Manual start
   dotnet run --project src/NetCrud.API/NetCrud.API
   ```

6. **Access the API**:
   - API: `http://localhost:5151`
   - Swagger UI: `http://localhost:5151/` (root URL)

## ⚡ Performance Tips

- **Use `dotnet watch`** for development - automatically restarts on changes
- **Use the provided scripts** (`./start.sh` or `./dev.sh`) for faster startup
- **Keep the app running** during development to avoid rebuild times
- **Use `dotnet build`** instead of `dotnet run` to check for errors without starting

## 📚 API Endpoints

### Users Controller

| Method | Endpoint           | Description          |
| ------ | ------------------ | -------------------- |
| GET    | `/api/users`       | Get all users        |
| GET    | `/api/users/{id}`  | Get user by ID       |
| POST   | `/api/users`       | Create new user      |
| PUT    | `/api/users/{id}`  | Update user          |
| DELETE | `/api/users/{id}`  | Delete user          |
| GET    | `/api/users/count` | Get total user count |

### Sample User Object

```json
{
  "id": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "+1234567890",
  "dateOfBirth": "1990-01-01T00:00:00Z",
  "address": "123 Main St, City, State",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}
```

## 🏗️ Project Structure

### Domain Layer (`NetCrud.Domain`)

- **Entities**: `BaseEntity`, `User`
- **Interfaces**: `IRepository<T>`, `IUnitOfWork`

### Application Layer (`NetCrud.Application`)

- **DTOs**: `UserDto`, `CreateUserDto`, `UpdateUserDto`
- **Services**: `UserService`
- **Interfaces**: `IUserService`

### Infrastructure Layer (`NetCrud.Infrastructure`)

- **Data**: `ApplicationDbContext`
- **Repositories**: `Repository<T>`, `UnitOfWork`
- **Extensions**: `ServiceCollectionExtensions`

### API Layer (`NetCrud.API`)

- **Controllers**: `UsersController`
- **Configuration**: `Program.cs`, `appsettings.json`

## 🔧 Configuration

### Database Configuration

The application uses Entity Framework Core with MySQL. The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=NetCrudDB;Uid=root;Pwd=breitling;Port=3306;"
  }
}
```

### Logging Configuration

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

## 🧪 Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/NetCrud.UnitTests
dotnet test tests/NetCrud.IntegrationTests
```

## 📝 Development

### Adding New Entities

1. **Create Entity** in `NetCrud.Domain/Entities`
2. **Add to DbContext** in `ApplicationDbContext`
3. **Create DTOs** in `NetCrud.Application/DTOs`
4. **Create Service Interface** in `NetCrud.Application/Interfaces`
5. **Implement Service** in `NetCrud.Application/Services`
6. **Create Controller** in `NetCrud.API/Controllers`
7. **Add Migration**:
   ```bash
   dotnet ef migrations add AddNewEntity
   dotnet ef database update
   ```

### Code Generation Commands

```bash
# Add new migration
dotnet ef migrations add MigrationName --project src/NetCrud.Infrastructure/NetCrud.Infrastructure

# Update database
dotnet ef database update --project src/NetCrud.Infrastructure/NetCrud.Infrastructure

# Remove last migration
dotnet ef migrations remove --project src/NetCrud.Infrastructure/NetCrud.Infrastructure
```

## 🔒 Security Considerations

- **Input Validation**: All inputs are validated using data annotations
- **SQL Injection**: Protected by Entity Framework Core parameterized queries
- **CORS**: Configured for development (should be restricted in production)
- **Connection Strings**: Store sensitive data in User Secrets or Azure Key Vault in production

## 🚀 Deployment

### Docker Support

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/NetCrud.API/NetCrud.API.csproj", "src/NetCrud.API/"]
COPY ["src/NetCrud.Application/NetCrud.Application.csproj", "src/NetCrud.Application/"]
COPY ["src/NetCrud.Domain/NetCrud.Domain.csproj", "src/NetCrud.Domain/"]
COPY ["src/NetCrud.Infrastructure/NetCrud.Infrastructure.csproj", "src/NetCrud.Infrastructure/"]
RUN dotnet restore "src/NetCrud.API/NetCrud.API.csproj"
COPY . .
WORKDIR "/src/src/NetCrud.API"
RUN dotnet build "NetCrud.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCrud.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCrud.API.dll"]
```

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📞 Support

If you have any questions or need help, please open an issue in the repository.

---

**Happy Coding! 🎉**
