using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;

namespace Tests.eShop.Domain.Interfaces.Repositories;

public class ICatalogItemRepositoryTests
{
    // Note: Interface testing focuses on contract validation and method signature verification
    // Actual implementation testing would be done in the concrete repository test classes

    [Fact]
    public void ICatalogItemRepository_HasGetAllAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.GetAllAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<CatalogItem>>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Single(parameters);
        Assert.Equal(typeof(CancellationToken), parameters[0].ParameterType);
        Assert.True(parameters[0].HasDefaultValue);
        Assert.Equal(default(CancellationToken), parameters[0].DefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasGetByIdAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.GetByIdAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogItem?>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasAddAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.AddAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogItem>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogItem), parameters[0].ParameterType);
        Assert.Equal("entity", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasUpdateAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.UpdateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogItem), parameters[0].ParameterType);
        Assert.Equal("entity", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasDeleteAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.DeleteAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasExistsAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.ExistsAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<bool>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasSearchAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.SearchAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<CatalogItem>>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(string), parameters[0].ParameterType);
        Assert.Equal("searchTerm", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasGetByBrandAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.GetByBrandAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<CatalogItem>>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("brandId", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasGetByTypeAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.GetByTypeAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<CatalogItem>>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("typeId", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_HasGetPagedAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogItemRepository.GetPagedAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<(IEnumerable<CatalogItem> Items, int TotalCount)>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(5, parameters.Length);

        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("pageIndex", parameters[0].Name);

        Assert.Equal(typeof(int), parameters[1].ParameterType);
        Assert.Equal("pageSize", parameters[1].Name);

        Assert.Equal(typeof(int?), parameters[2].ParameterType);
        Assert.Equal("brandId", parameters[2].Name);
        Assert.True(parameters[2].HasDefaultValue);
        Assert.Null(parameters[2].DefaultValue);

        Assert.Equal(typeof(int?), parameters[3].ParameterType);
        Assert.Equal("typeId", parameters[3].Name);
        Assert.True(parameters[3].HasDefaultValue);
        Assert.Null(parameters[3].DefaultValue);

        Assert.Equal(typeof(CancellationToken), parameters[4].ParameterType);
        Assert.True(parameters[4].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemRepository_IsInterface()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act & Assert
        Assert.True(repositoryType.IsInterface);
        Assert.True(repositoryType.IsPublic);
    }

    [Fact]
    public void ICatalogItemRepository_HasCorrectMethodCount()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act
        var methods = repositoryType.GetMethods();

        // Assert - Should have exactly 9 methods (no inherited methods from object in interfaces)
        Assert.Equal(9, methods.Length);
    }

    [Fact]
    public void ICatalogItemRepository_AllMethodsAreAsync()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);
        var methods = repositoryType.GetMethods();

        // Act & Assert
        foreach (var method in methods)
        {
            Assert.True(method.Name.EndsWith("Async"), $"Method {method.Name} should end with 'Async'");
            Assert.True(typeof(Task).IsAssignableFrom(method.ReturnType),
                $"Method {method.Name} should return Task or Task<T>");
        }
    }

    [Fact]
    public void ICatalogItemRepository_AllMethodsHaveCancellationToken()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);
        var methods = repositoryType.GetMethods();

        // Act & Assert
        foreach (var method in methods)
        {
            var parameters = method.GetParameters();
            var hasCancellationToken = Array.Exists(parameters, p => p.ParameterType == typeof(CancellationToken));
            Assert.True(hasCancellationToken, $"Method {method.Name} should have CancellationToken parameter");

            // The last parameter should be CancellationToken
            var lastParam = parameters[parameters.Length - 1];
            Assert.Equal(typeof(CancellationToken), lastParam.ParameterType);
        }
    }

    [Fact]
    public void ICatalogItemRepository_IsInCorrectNamespace()
    {
        // Arrange
        var repositoryType = typeof(ICatalogItemRepository);

        // Act & Assert
        Assert.Equal("eShop.Domain.Interfaces.Repositories", repositoryType.Namespace);
    }
}