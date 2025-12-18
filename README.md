# eShop - .NET 8 Migration

This project has been successfully migrated from ASP.NET Web Forms 4.7.2 to .NET 8 using clean architecture principles.

## Architecture

The application follows clean architecture with the following layers:

- **Domain Layer** (`eShop.Domain`): Contains entities, interfaces, and domain exceptions
- **Application Layer** (`eShop.Application`): Contains business logic, services, and DTOs
- **Infrastructure Layer** (`eShop.Infrastructure`): Contains data access, EF Core, and repositories
- **Web Layer** (`eShop.Web`): Contains Razor Pages UI and presentation logic

## Technology Stack

- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core 8.0
- SQL Server
- Serilog for logging
- Bootstrap 5 for UI

## Setup Instructions

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or SQL Server instance)

### Database Setup

1. Update the connection string in `src/eShop.Web/appsettings.json` if needed
2. Run Entity Framework migrations:

```bash
cd src/eShop.Web
dotnet ef migrations add InitialCreate --project ../eShop.Infrastructure
dotnet ef database update
```

### Running the Application

```bash
cd src/eShop.Web
dotnet run
```

The application will be available at `https://localhost:5001` (or the port specified in launchSettings.json)

## Project Structure

```
eShop/
├── src/
│   ├── eShop.Domain/           # Domain entities and interfaces
│   ├── eShop.Application/      # Business logic and services
│   ├── eShop.Infrastructure/   # Data access and EF Core
│   └── eShop.Web/             # Razor Pages UI
└── docs/                      # Documentation
```

## Key Features

- Complete CRUD operations for catalog items
- Filtering by brand and type
- Search functionality
- Pagination support
- Clean architecture with proper separation of concerns
- Async/await throughout
- Comprehensive logging with Serilog
- Entity Framework Core with repository pattern

## Migration Changes

### What Was Changed

1. **Framework**: Migrated from .NET Framework 4.7.2 to .NET 8
2. **UI**: Converted from Web Forms (.aspx) to Razor Pages (.cshtml)
3. **Data Access**: Replaced Entity Framework 6 with EF Core 8.0
4. **Logging**: Replaced log4net with Serilog
5. **Configuration**: Moved from Web.config to appsettings.json
6. **DI**: Replaced Autofac with built-in ASP.NET Core DI
7. **Architecture**: Implemented clean architecture with four distinct layers

### Breaking Changes from Web Forms

- ViewState is no longer available (not needed with Razor Pages)
- Page lifecycle events replaced with OnGet/OnPost handlers
- Server controls replaced with HTML helpers and tag helpers
- Global.asax logic moved to Program.cs
- Session state requires explicit configuration if needed

## Testing

Build and verify the solution:

```bash
dotnet build
dotnet test
```

## License

Copyright © 2024 eShop
