# Migration Notes: ASP.NET Web Forms to .NET 8

## Migration Summary

Successfully migrated eShopLegacyWebForms from ASP.NET Web Forms 4.7.2 to .NET 8 with clean architecture.

**Migration Date**: 2025-12-18
**Source Framework**: .NET Framework 4.7.2 (ASP.NET Web Forms)
**Target Framework**: .NET 8
**Architecture Pattern**: Clean Architecture with 4 layers

## Components Migrated

### Pages Migrated

| Old Web Form | New Razor Page | Status |
|--------------|----------------|--------|
| Default.aspx | Pages/Index.cshtml | ✅ Complete |
| Default.aspx | Pages/Catalog/Index.cshtml | ✅ Complete |
| Catalog/Create.aspx | Pages/Catalog/Create.cshtml | ✅ Complete |
| Catalog/Edit.aspx | Pages/Catalog/Edit.cshtml | ✅ Complete |
| Catalog/Details.aspx | Pages/Catalog/Details.cshtml | ✅ Complete |
| Catalog/Delete.aspx | Pages/Catalog/Delete.cshtml | ✅ Complete |
| About.aspx | Pages/Privacy.cshtml | ✅ Complete |
| Site.Master | Pages/Shared/_Layout.cshtml | ✅ Complete |

### Entities Migrated

- **CatalogItem**: Migrated with additional audit fields (CreatedDate, ModifiedDate, IsActive)
- **CatalogBrand**: Migrated with audit fields and navigation properties
- **CatalogType**: Migrated with audit fields and navigation properties

### Data Access Migration

**Before (EF6)**:
- DbContext with EntityFramework 6.2.0
- Synchronous operations
- Database initializers
- Manual entity configurations

**After (EF Core 8)**:
- DbContext with EF Core 8.0.0
- Async operations throughout
- Migration-based approach
- IEntityTypeConfiguration for configurations
- Repository pattern implemented

### Dependency Injection Migration

**Before**:
- Autofac 4.9.1
- Autofac.Integration.Web 4.0.0
- Property injection in pages

**After**:
- Built-in ASP.NET Core DI
- Constructor injection throughout
- Scoped lifetimes for repositories and services

### Logging Migration

**Before**:
- log4net 2.0.10
- XML configuration (log4Net.xml)
- Static logger instances

**After**:
- Serilog.AspNetCore 8.0.0
- appsettings.json configuration
- ILogger<T> injection

### Configuration Migration

**Before (Web.config)**:
```xml
<connectionStrings>
  <add name="CatalogDBContext" connectionString="..." />
</connectionStrings>
<appSettings>
  <add key="UseMockData" value="true" />
</appSettings>
```

**After (appsettings.json)**:
```json
{
  "ConnectionStrings": {
    "CatalogConnection": "..."
  }
}
```

## Key Differences from Web Forms

### Page Lifecycle

**Web Forms**:
- Page_Init → Page_Load → Control Events → Page_PreRender → Page_Render
- ViewState management
- PostBack events

**Razor Pages**:
- OnGet() for GET requests
- OnPost() for POST requests
- No ViewState (model binding instead)
- Cleaner request handling

### Data Binding

**Web Forms**:
```csharp
ListView1.DataSource = catalogItems;
ListView1.DataBind();
```

**Razor Pages**:
```csharp
public IEnumerable<CatalogItem> Items { get; set; }

public async Task OnGetAsync()
{
    Items = await _service.GetAllAsync();
}
```

### Routing

**Web Forms**:
```csharp
RouteConfig.RegisterRoutes(RouteTable.Routes);
routes.MapPageRoute("catalog", "catalog", "~/Default.aspx");
```

**Razor Pages**:
- Convention-based routing
- Pages/Catalog/Index.cshtml → /Catalog/Index
- Route parameters via @page directive

## Breaking Changes

### 1. System.Web Dependencies Removed

- ❌ `HttpContext.Current` → ✅ `IHttpContextAccessor`
- ❌ `Server.MapPath()` → ✅ `IWebHostEnvironment.ContentRootPath`
- ❌ `Response.Redirect()` → ✅ `RedirectToPage()`
- ❌ `Session["key"]` → ✅ Configure session state explicitly

### 2. Entity Framework Changes

- ❌ `DbContext.Database.SqlQuery<T>()` → ✅ `DbContext.Set<T>().FromSqlRaw()`
- ❌ `DbSet.Add()` synchronous → ✅ `DbSet.AddAsync()` asynchronous
- ❌ Database initializers → ✅ Migrations
- ❌ `HasRequired().WithMany()` → ✅ `HasOne().WithMany()`

### 3. Configuration Changes

- ❌ `ConfigurationManager.AppSettings["key"]` → ✅ `IConfiguration["key"]`
- ❌ `ConfigurationManager.ConnectionStrings["name"]` → ✅ `IConfiguration.GetConnectionString("name")`

### 4. Bundling and Minification

- ❌ `BundleConfig.RegisterBundles()` → ✅ Static file middleware with CDN references
- ❌ `@Scripts.Render("~/bundles/jquery")` → ✅ `<script src="~/lib/jquery/dist/jquery.min.js"></script>`

## Known Issues

### 1. Client-Side Libraries

The project uses CDN references for Bootstrap and jQuery. To use local copies:

```bash
cd src/eShop.Web
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
libman init
libman install bootstrap@5.3.0 -d wwwroot/lib/bootstrap
libman install jquery@3.7.1 -d wwwroot/lib/jquery
libman install jquery-validation@1.19.5 -d wwwroot/lib/jquery-validation
libman install jquery-validation-unobtrusive@4.0.0 -d wwwroot/lib/jquery-validation-unobtrusive
```

### 2. Database Seeding

The original Web Forms app used database initializers. In the new version, you'll need to seed data manually or create a migration with seed data.

### 3. Image Files

Product images are referenced from `wwwroot/pics/`. Ensure all image files are copied from the old `Pics` directory.

## Performance Improvements

1. **Async/Await**: All database operations are now asynchronous
2. **AsNoTracking**: Read-only queries use AsNoTracking for better performance
3. **Connection Resiliency**: EF Core configured with retry logic for transient failures
4. **Pagination**: Implemented proper server-side pagination

## Security Improvements

1. **ValidateRequest**: Enabled by default (no longer needs to be disabled)
2. **CSRF Protection**: Built into Razor Pages by default
3. **HTTPS**: Enforced by default
4. **Null Safety**: Enabled nullable reference types throughout

## Testing Recommendations

1. Test all CRUD operations for catalog items
2. Verify filtering and search functionality
3. Test pagination with different page sizes
4. Verify image loading
5. Test error handling and validation
6. Load test with larger datasets

## Future Improvements

1. Add authentication and authorization
2. Implement caching for frequently accessed data
3. Add health checks
4. Implement API endpoints for mobile apps
5. Add comprehensive unit and integration tests
6. Implement distributed caching with Redis
7. Add application insights/monitoring

## References

- [Migrate from ASP.NET to ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/migration/proper-to-2x/)
- [EF Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Razor Pages Documentation](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/)
