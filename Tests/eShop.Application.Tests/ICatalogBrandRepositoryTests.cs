using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;

namespace Tests.eShop.Domain.Interfaces.Repositories;

public class ICatalogBrandRepositoryTests
{
    [Fact]
    public void ICatalogBrandRepository_HasGetAllAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogBrandRepository.GetAllAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<CatalogBrand>>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Single(parameters);
        Assert.Equal(typeof(CancellationToken), parameters[0].ParameterType);
        Assert.True(parameters[0].HasDefaultValue);
        Assert.Equal(default(CancellationToken), parameters[0].DefaultValue);
    }

    [Fact]
    public void ICatalogBrandRepository_HasGetByIdAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogBrandRepository.GetByIdAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogBrand?>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogBrandRepository_HasAddAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogBrandRepository.AddAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogBrand>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogBrand), parameters[0].ParameterType);
        Assert.Equal("entity", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogBrandRepository_HasUpdateAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogBrandRepository.UpdateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogBrand), parameters[0].ParameterType);
        Assert.Equal("entity", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogBrandRepository_HasDeleteAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogBrandRepository.DeleteAsync));

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
    public void ICatalogBrandRepository_HasExistsAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogBrandRepository.ExistsAsync));

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
    public void ICatalogBrandRepository_HasSearchAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogBrandRepository.SearchAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<CatalogBrand>>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(string), parameters[0].ParameterType);
        Assert.Equal("searchTerm", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogBrandRepository_IsInterface()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act & Assert
        Assert.True(repositoryType.IsInterface);
        Assert.True(repositoryType.IsPublic);
    }

    [Fact]
    public void ICatalogBrandRepository_HasCorrectMethodCount()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var methods = repositoryType.GetMethods();

        // Assert - Should have exactly 7 methods
        Assert.Equal(7, methods.Length);
    }

    [Fact]
    public void ICatalogBrandRepository_AllMethodsAreAsync()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);
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
    public void ICatalogBrandRepository_AllMethodsHaveCancellationToken()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);
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
    public void ICatalogBrandRepository_IsInCorrectNamespace()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act & Assert
        Assert.Equal("eShop.Domain.Interfaces.Repositories", repositoryType.Namespace);
    }

    [Fact]
    public void ICatalogBrandRepository_MethodsHaveCorrectSignatures()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act & Assert - Verify all methods exist with expected signatures
        var getAllMethod = repositoryType.GetMethod("GetAllAsync");
        var getByIdMethod = repositoryType.GetMethod("GetByIdAsync");
        var addMethod = repositoryType.GetMethod("AddAsync");
        var updateMethod = repositoryType.GetMethod("UpdateAsync");
        var deleteMethod = repositoryType.GetMethod("DeleteAsync");
        var existsMethod = repositoryType.GetMethod("ExistsAsync");
        var searchMethod = repositoryType.GetMethod("SearchAsync");

        Assert.NotNull(getAllMethod);
        Assert.NotNull(getByIdMethod);
        Assert.NotNull(addMethod);
        Assert.NotNull(updateMethod);
        Assert.NotNull(deleteMethod);
        Assert.NotNull(existsMethod);
        Assert.NotNull(searchMethod);
    }

    [Fact]
    public void ICatalogBrandRepository_CRUDOperationsExist()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var hasCreate = repositoryType.GetMethod("AddAsync") != null;
        var hasRead = repositoryType.GetMethod("GetAllAsync") != null &&
                      repositoryType.GetMethod("GetByIdAsync") != null;
        var hasUpdate = repositoryType.GetMethod("UpdateAsync") != null;
        var hasDelete = repositoryType.GetMethod("DeleteAsync") != null;

        // Assert
        Assert.True(hasCreate, "Repository should have Create operation (AddAsync)");
        Assert.True(hasRead, "Repository should have Read operations (GetAllAsync, GetByIdAsync)");
        Assert.True(hasUpdate, "Repository should have Update operation (UpdateAsync)");
        Assert.True(hasDelete, "Repository should have Delete operation (DeleteAsync)");
    }

    [Fact]
    public void ICatalogBrandRepository_HasSearchCapability()
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var searchMethod = repositoryType.GetMethod("SearchAsync");
        var existsMethod = repositoryType.GetMethod("ExistsAsync");

        // Assert
        Assert.NotNull(searchMethod);
        Assert.NotNull(existsMethod);
    }

    [Theory]
    [InlineData("GetAllAsync")]
    [InlineData("GetByIdAsync")]
    [InlineData("AddAsync")]
    [InlineData("UpdateAsync")]
    [InlineData("DeleteAsync")]
    [InlineData("ExistsAsync")]
    [InlineData("SearchAsync")]
    public void ICatalogBrandRepository_HasExpectedMethod(string methodName)
    {
        // Arrange
        var repositoryType = typeof(ICatalogBrandRepository);

        // Act
        var method = repositoryType.GetMethod(methodName);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.Name.EndsWith("Async"));
    }
}