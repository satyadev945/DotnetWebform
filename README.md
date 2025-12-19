# eShopModern - Migrated from Web Forms to .NET 8

This project has been successfully migrated from ASP.NET Web Forms to a modern .NET 8 application using clean architecture principles.

## Migration Summary

- **Original**: ASP.NET Web Forms 4.7.2
- **Target**: .NET 8 with clean architecture
- **Migration Date**: 2025-12-19

## Architecture Overview

The migrated application follows clean architecture principles with clear separation of concerns:

### Layer Structure

```
├── src/
│   ├── eShopModern.Domain/         # Domain entities and interfaces
│   ├── eShopModern.Application/    # Business logic and DTOs
│   ├── eShopModern.Infrastructure/ # Data access and external services
│   └── eShopModern.Web/           # ASP.NET Core Razor Pages
└── tests/
    ├── eShopModern.UnitTests/      # Unit tests
    └── eShopModern.IntegrationTests/ # Integration tests
```

### Technologies Used

- **Framework**: .NET 8
- **Web Framework**: ASP.NET Core with Razor Pages
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server (configurable)
- **Logging**: Serilog
- **Mapping**: AutoMapper
- **Testing**: xUnit, FluentAssertions, Moq

## Migration Changes

### Key Migrations Performed

1. **Project Structure**
   - Converted from Web Forms to clean architecture
   - Created separate projects for Domain, Application, Infrastructure, and Web layers

2. **Data Access**
   - Migrated from Entity Framework 6 to EF Core 8.0
   - Implemented repository pattern with proper async/await

3. **Web Layer**
   - Converted .aspx pages to Razor Pages
   - Replaced master pages with layout pages
   - Updated server controls to HTML helpers and Tag Helpers

4. **Configuration**
   - Migrated Web.config to appsettings.json
   - Replaced Global.asax with Program.cs startup configuration

5. **Logging**
   - Replaced log4net with Serilog for structured logging

6. **Dependency Injection**
   - Implemented built-in ASP.NET Core DI container
   - Replaced Autofac with native DI

## Database Setup

1. Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=eShopModern;Trusted_Connection=true;"
  }
}
```

2. The application will automatically create the database on first run.

## Running the Application

1. **Prerequisites**:
   - .NET 8 SDK
   - SQL Server (LocalDB or full instance)

2. **Build and Run**:
```bash
dotnet restore
dotnet build
dotnet run --project src/eShopModern.Web
```

3. **Access the application**:
   - Navigate to `https://localhost:5001` or `http://localhost:5000`

## Testing

Run unit tests:
```bash
dotnet test tests/eShopModern.UnitTests
```

Run integration tests:
```bash
dotnet test tests/eShopModern.IntegrationTests
```

## Features

The migrated application maintains all original functionality:

- Catalog item management (CRUD operations)
- Product listing with pagination
- Image management
- Brand and category management
- Responsive design with Bootstrap

## Migration Notes

### What Was Changed

1. **UI Framework**: Web Forms → Razor Pages
2. **Data Access**: EF 6 → EF Core 8
3. **Logging**: log4net → Serilog
4. **Configuration**: Web.config → appsettings.json
5. **Architecture**: Monolithic → Clean Architecture

### Breaking Changes

- ViewState is no longer available (replaced with modern state management)
- Server controls replaced with standard HTML and Tag Helpers
- Postback model replaced with modern request/response patterns

### Known Issues

- Some advanced Web Forms features may require manual review
- Custom controls need individual assessment
- Complex ViewState dependencies may need refactoring

## Future Improvements

1. Add authentication and authorization
2. Implement caching strategies
3. Add API endpoints for mobile/SPA applications
4. Enhance error handling and validation
5. Add more comprehensive tests
6. Implement health checks and monitoring

## Support

For issues and questions related to this migration, please review the migration logs and documentation in the `docs/` directory.