# eShop - Migrated to .NET 8

This project has been migrated from ASP.NET Web Forms 4.7.2 to .NET 8 using Clean Architecture principles.

## Architecture

The solution is structured into four main layers:

- **eShop.Domain**: Core business entities and interfaces
- **eShop.Application**: Business logic and service implementations
- **eShop.Infrastructure**: Data access with Entity Framework Core 8
- **eShop.Web**: Razor Pages UI layer

## Prerequisites

- .NET 8 SDK
- SQL Server or LocalDB

## Configuration

Update the connection string in `src/eShop.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=eShopDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

## Running the Application

```bash
cd /modernize-data/studio-data/TNT1001/APP1970/transformed-code/87/studio-workspace/dotnetcomp
dotnet restore
dotnet build
dotnet run --project src/eShop.Web
```

## Migration Notes

### Key Changes

1. **Framework**: Migrated from .NET Framework 4.7.2 to .NET 8
2. **UI**: Converted from ASP.NET Web Forms to Razor Pages
3. **Data Access**: Replaced Entity Framework 6 with EF Core 8
4. **Logging**: Replaced log4net with Serilog
5. **Configuration**: Migrated from Web.config to appsettings.json
6. **Dependency Injection**: Using built-in ASP.NET Core DI container

### Features

- Complete CRUD operations for catalog items
- Pagination and filtering
- Clean architecture with proper separation of concerns
- Async/await patterns throughout
- Repository pattern for data access
- Service layer for business logic

## Build Status

✅ Build successful with .NET 8
