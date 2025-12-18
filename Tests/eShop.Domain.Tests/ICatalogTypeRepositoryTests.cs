using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;

namespace Tests.eShop.Domain.Interfaces.Repositories;

public class ICatalogTypeRepositoryTests
{
    [Fact]
    public void ICatalogTypeRepository_HasGetAllAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogTypeRepository.GetAllAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<CatalogType>>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Single(parameters);
        Assert.Equal(typeof(CancellationToken), parameters[0].ParameterType);
        Assert.True(parameters[0].HasDefaultValue);
        Assert.Equal(default(CancellationToken), parameters[0].DefaultValue);
    }

    [Fact]
    public void ICatalogTypeRepository_HasGetByIdAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogTypeRepository.GetByIdAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogType?>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogTypeRepository_HasAddAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogTypeRepository.AddAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogType>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogType), parameters[0].ParameterType);
        Assert.Equal("entity", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogTypeRepository_HasUpdateAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogTypeRepository.UpdateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogType), parameters[0].ParameterType);
        Assert.Equal("entity", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogTypeRepository_HasDeleteAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogTypeRepository.DeleteAsync));

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
    public void ICatalogTypeRepository_HasExistsAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogTypeRepository.ExistsAsync));

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
    public void ICatalogTypeRepository_HasSearchAsyncMethod()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var method = repositoryType.GetMethod(nameof(ICatalogTypeRepository.SearchAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<CatalogType>>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(string), parameters[0].ParameterType);
        Assert.Equal("searchTerm", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogTypeRepository_IsInterface()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act & Assert
        Assert.True(repositoryType.IsInterface);
        Assert.True(repositoryType.IsPublic);
    }

    [Fact]
    public void ICatalogTypeRepository_HasCorrectMethodCount()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var methods = repositoryType.GetMethods();

        // Assert - Should have exactly 7 methods
        Assert.Equal(7, methods.Length);
    }

    [Fact]
    public void ICatalogTypeRepository_AllMethodsAreAsync()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);
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
    public void ICatalogTypeRepository_AllMethodsHaveCancellationToken()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);
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
    public void ICatalogTypeRepository_IsInCorrectNamespace()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act & Assert
        Assert.Equal("eShop.Domain.Interfaces.Repositories", repositoryType.Namespace);
    }

    [Fact]
    public void ICatalogTypeRepository_MethodsHaveCorrectSignatures()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

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
    public void ICatalogTypeRepository_CRUDOperationsExist()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

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
    public void ICatalogTypeRepository_HasSearchCapability()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

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
    public void ICatalogTypeRepository_HasExpectedMethod(string methodName)
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var method = repositoryType.GetMethod(methodName);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.Name.EndsWith("Async"));
    }

    [Fact]
    public void ICatalogTypeRepository_GenericConstraintsAreCorrect()
    {
        // Arrange
        var repositoryType = typeof(ICatalogTypeRepository);

        // Act
        var getAllMethod = repositoryType.GetMethod("GetAllAsync");
        var getByIdMethod = repositoryType.GetMethod("GetByIdAsync");
        var addMethod = repositoryType.GetMethod("AddAsync");
        var updateMethod = repositoryType.GetMethod("UpdateAsync");
        var searchMethod = repositoryType.GetMethod("SearchAsync");

        // Assert - All methods work with CatalogType entity
        Assert.True(getAllMethod!.ReturnType.GetGenericArguments().Any(t =>
            t.GetGenericArguments().Contains(typeof(CatalogType))));

        Assert.Equal(typeof(CatalogType),
            getByIdMethod!.ReturnType.GetGenericArguments()[0].GetGenericArguments()[0]);

        Assert.Equal(typeof(CatalogType),
            addMethod!.GetParameters()[0].ParameterType);

        Assert.Equal(typeof(CatalogType),
            updateMethod!.GetParameters()[0].ParameterType);

        Assert.True(searchMethod!.ReturnType.GetGenericArguments().Any(t =>
            t.GetGenericArguments().Contains(typeof(CatalogType))));
    }
}