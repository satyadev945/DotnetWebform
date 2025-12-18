using Xunit;
using Microsoft.Extensions.DependencyInjection;
using eShop.Application.Extensions;
using eShop.Application.Services;
using eShop.Domain.Interfaces.Services;
using System;

namespace Tests.eShop.Application.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddApplicationServices_WithValidServiceCollection_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddApplicationServices();

        // Assert
        Assert.NotNull(result);
        Assert.Same(services, result); // Should return the same collection for fluent chaining
    }

    [Fact]
    public void AddApplicationServices_RegistersICatalogItemService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(ICatalogItemService));
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(typeof(CatalogItemService), serviceDescriptor!.ImplementationType);
        Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
    }

    [Fact]
    public void AddApplicationServices_RegistersICatalogBrandService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(ICatalogBrandService));
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(typeof(CatalogBrandService), serviceDescriptor!.ImplementationType);
        Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
    }

    [Fact]
    public void AddApplicationServices_RegistersICatalogTypeService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(ICatalogTypeService));
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(typeof(CatalogTypeService), serviceDescriptor!.ImplementationType);
        Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
    }

    [Fact]
    public void AddApplicationServices_RegistersAllThreeServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Equal(3, services.Count);

        var catalogItemService = services.FirstOrDefault(s => s.ServiceType == typeof(ICatalogItemService));
        var catalogBrandService = services.FirstOrDefault(s => s.ServiceType == typeof(ICatalogBrandService));
        var catalogTypeService = services.FirstOrDefault(s => s.ServiceType == typeof(ICatalogTypeService));

        Assert.NotNull(catalogItemService);
        Assert.NotNull(catalogBrandService);
        Assert.NotNull(catalogTypeService);
    }

    [Fact]
    public void AddApplicationServices_CanBeCalledMultipleTimes()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();
        services.AddApplicationServices();

        // Assert
        // Should have 6 registrations (3 services x 2 calls)
        Assert.Equal(6, services.Count);
    }

    [Fact]
    public void AddApplicationServices_WithNullServiceCollection_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddApplicationServices());
    }

    [Fact]
    public void AddApplicationServices_AllServicesHaveScopedLifetime()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        foreach (var serviceDescriptor in services)
        {
            Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
        }
    }

    [Fact]
    public void AddApplicationServices_CanBeUsedForFluentChaining()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act & Assert - Should not throw and allow chaining
        var result = services
            .AddApplicationServices()
            .AddSingleton<string>("test");

        Assert.Equal(4, services.Count); // 3 application services + 1 string service
    }

    [Fact]
    public void AddApplicationServices_ServiceTypesMatchInterfaces()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        foreach (var serviceDescriptor in services)
        {
            Assert.True(serviceDescriptor.ServiceType.IsInterface,
                $"Service type {serviceDescriptor.ServiceType.Name} should be an interface");
            Assert.True(serviceDescriptor.ImplementationType != null,
                "Implementation type should not be null");
            Assert.True(serviceDescriptor.ServiceType.IsAssignableFrom(serviceDescriptor.ImplementationType!),
                $"Implementation {serviceDescriptor.ImplementationType!.Name} should implement {serviceDescriptor.ServiceType.Name}");
        }
    }

    [Fact]
    public void AddApplicationServices_RegistersConcreteImplementations()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        var catalogItemServiceDescriptor = services.First(s => s.ServiceType == typeof(ICatalogItemService));
        var catalogBrandServiceDescriptor = services.First(s => s.ServiceType == typeof(ICatalogBrandService));
        var catalogTypeServiceDescriptor = services.First(s => s.ServiceType == typeof(ICatalogTypeService));

        Assert.False(catalogItemServiceDescriptor.ImplementationType!.IsInterface);
        Assert.False(catalogBrandServiceDescriptor.ImplementationType!.IsInterface);
        Assert.False(catalogTypeServiceDescriptor.ImplementationType!.IsInterface);

        Assert.False(catalogItemServiceDescriptor.ImplementationType.IsAbstract);
        Assert.False(catalogBrandServiceDescriptor.ImplementationType.IsAbstract);
        Assert.False(catalogTypeServiceDescriptor.ImplementationType.IsAbstract);
    }

    [Fact]
    public void ServiceCollectionExtensions_IsStaticClass()
    {
        // Arrange
        var type = typeof(ServiceCollectionExtensions);

        // Act & Assert
        Assert.True(type.IsClass);
        Assert.True(type.IsSealed);
        Assert.True(type.IsAbstract); // Static classes are marked as abstract in IL
    }

    [Fact]
    public void AddApplicationServices_IsExtensionMethod()
    {
        // Arrange
        var method = typeof(ServiceCollectionExtensions).GetMethod(nameof(ServiceCollectionExtensions.AddApplicationServices));

        // Act & Assert
        Assert.NotNull(method);
        Assert.True(method!.IsStatic);
        Assert.True(method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false));
    }

    [Fact]
    public void AddApplicationServices_ReturnsIServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddApplicationServices();

        // Assert
        Assert.IsAssignableFrom<IServiceCollection>(result);
    }

    [Fact]
    public void ServiceCollectionExtensions_IsInCorrectNamespace()
    {
        // Arrange
        var type = typeof(ServiceCollectionExtensions);

        // Act & Assert
        Assert.Equal("eShop.Application.Extensions", type.Namespace);
    }

    [Fact]
    public void AddApplicationServices_ServicesAreRegisteredInCorrectOrder()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        var serviceTypes = services.Select(s => s.ServiceType).ToArray();

        Assert.Equal(typeof(ICatalogItemService), serviceTypes[0]);
        Assert.Equal(typeof(ICatalogBrandService), serviceTypes[1]);
        Assert.Equal(typeof(ICatalogTypeService), serviceTypes[2]);
    }
}