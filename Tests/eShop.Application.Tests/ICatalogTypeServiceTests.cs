using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;

namespace Tests.eShop.Domain.Interfaces.Services;

public class ICatalogTypeServiceTests
{
    [Fact]
    public void ICatalogTypeService_HasGetAllAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogTypeService.GetAllAsync));

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
    public void ICatalogTypeService_HasGetByIdAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogTypeService.GetByIdAsync));

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
    public void ICatalogTypeService_HasCreateAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogTypeService.CreateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogType>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogType), parameters[0].ParameterType);
        Assert.Equal("type", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogTypeService_HasUpdateAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogTypeService.UpdateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(3, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);
        Assert.Equal(typeof(CatalogType), parameters[1].ParameterType);
        Assert.Equal("type", parameters[1].Name);
        Assert.Equal(typeof(CancellationToken), parameters[2].ParameterType);
        Assert.True(parameters[2].HasDefaultValue);
    }

    [Fact]
    public void ICatalogTypeService_HasDeleteAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogTypeService.DeleteAsync));

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
    public void ICatalogTypeService_HasSearchAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogTypeService.SearchAsync));

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
    public void ICatalogTypeService_IsInterface()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act & Assert
        Assert.True(serviceType.IsInterface);
        Assert.True(serviceType.IsPublic);
    }

    [Fact]
    public void ICatalogTypeService_HasCorrectMethodCount()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var methods = serviceType.GetMethods();

        // Assert - Should have exactly 6 methods
        Assert.Equal(6, methods.Length);
    }

    [Fact]
    public void ICatalogTypeService_AllMethodsAreAsync()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);
        var methods = serviceType.GetMethods();

        // Act & Assert
        foreach (var method in methods)
        {
            Assert.True(method.Name.EndsWith("Async"), $"Method {method.Name} should end with 'Async'");
            Assert.True(typeof(Task).IsAssignableFrom(method.ReturnType),
                $"Method {method.Name} should return Task or Task<T>");
        }
    }

    [Fact]
    public void ICatalogTypeService_AllMethodsHaveCancellationToken()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);
        var methods = serviceType.GetMethods();

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
    public void ICatalogTypeService_IsInCorrectNamespace()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act & Assert
        Assert.Equal("eShop.Domain.Interfaces.Services", serviceType.Namespace);
    }

    [Fact]
    public void ICatalogTypeService_HasBusinessOperations()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var hasCreate = serviceType.GetMethod("CreateAsync") != null;
        var hasRead = serviceType.GetMethod("GetAllAsync") != null &&
                      serviceType.GetMethod("GetByIdAsync") != null;
        var hasUpdate = serviceType.GetMethod("UpdateAsync") != null;
        var hasDelete = serviceType.GetMethod("DeleteAsync") != null;
        var hasSearch = serviceType.GetMethod("SearchAsync") != null;

        // Assert
        Assert.True(hasCreate, "Service should have Create operation (CreateAsync)");
        Assert.True(hasRead, "Service should have Read operations (GetAllAsync, GetByIdAsync)");
        Assert.True(hasUpdate, "Service should have Update operation (UpdateAsync)");
        Assert.True(hasDelete, "Service should have Delete operation (DeleteAsync)");
        Assert.True(hasSearch, "Service should have Search capability (SearchAsync)");
    }

    [Theory]
    [InlineData("GetAllAsync")]
    [InlineData("GetByIdAsync")]
    [InlineData("CreateAsync")]
    [InlineData("UpdateAsync")]
    [InlineData("DeleteAsync")]
    [InlineData("SearchAsync")]
    public void ICatalogTypeService_HasExpectedMethod(string methodName)
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var method = serviceType.GetMethod(methodName);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.Name.EndsWith("Async"));
    }

    [Fact]
    public void ICatalogTypeService_UpdateMethodTakesIdAndType()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var updateMethod = serviceType.GetMethod("UpdateAsync");

        // Assert
        Assert.NotNull(updateMethod);
        var parameters = updateMethod!.GetParameters();
        Assert.Equal(3, parameters.Length); // id, type, cancellationToken

        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);

        Assert.Equal(typeof(CatalogType), parameters[1].ParameterType);
        Assert.Equal("type", parameters[1].Name);
    }

    [Fact]
    public void ICatalogTypeService_CreateMethodReturnsCreatedType()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var createMethod = serviceType.GetMethod("CreateAsync");

        // Assert
        Assert.NotNull(createMethod);
        Assert.Equal(typeof(Task<CatalogType>), createMethod!.ReturnType);
    }

    [Fact]
    public void ICatalogTypeService_SearchAcceptsStringParameter()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var searchMethod = serviceType.GetMethod("SearchAsync");

        // Assert
        Assert.NotNull(searchMethod);
        var parameters = searchMethod!.GetParameters();
        Assert.Equal(typeof(string), parameters[0].ParameterType);
        Assert.Equal("searchTerm", parameters[0].Name);
    }

    [Fact]
    public void ICatalogTypeService_MethodsHaveCorrectSignatures()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act & Assert - Verify all methods exist with expected signatures
        var getAllMethod = serviceType.GetMethod("GetAllAsync");
        var getByIdMethod = serviceType.GetMethod("GetByIdAsync");
        var createMethod = serviceType.GetMethod("CreateAsync");
        var updateMethod = serviceType.GetMethod("UpdateAsync");
        var deleteMethod = serviceType.GetMethod("DeleteAsync");
        var searchMethod = serviceType.GetMethod("SearchAsync");

        Assert.NotNull(getAllMethod);
        Assert.NotNull(getByIdMethod);
        Assert.NotNull(createMethod);
        Assert.NotNull(updateMethod);
        Assert.NotNull(deleteMethod);
        Assert.NotNull(searchMethod);
    }

    [Fact]
    public void ICatalogTypeService_CreateMethodParameterNaming()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var createMethod = serviceType.GetMethod("CreateAsync");

        // Assert
        Assert.NotNull(createMethod);
        var parameters = createMethod!.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal("type", parameters[0].Name);
        Assert.Equal(typeof(CatalogType), parameters[0].ParameterType);
    }

    [Fact]
    public void ICatalogTypeService_AllCRUDOperationsReturnCorrectTypes()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var createMethod = serviceType.GetMethod("CreateAsync");
        var getAllMethod = serviceType.GetMethod("GetAllAsync");
        var getByIdMethod = serviceType.GetMethod("GetByIdAsync");
        var updateMethod = serviceType.GetMethod("UpdateAsync");
        var deleteMethod = serviceType.GetMethod("DeleteAsync");
        var searchMethod = serviceType.GetMethod("SearchAsync");

        // Assert
        Assert.Equal(typeof(Task<CatalogType>), createMethod!.ReturnType);
        Assert.Equal(typeof(Task<IEnumerable<CatalogType>>), getAllMethod!.ReturnType);
        Assert.Equal(typeof(Task<CatalogType?>), getByIdMethod!.ReturnType);
        Assert.Equal(typeof(Task), updateMethod!.ReturnType);
        Assert.Equal(typeof(Task), deleteMethod!.ReturnType);
        Assert.Equal(typeof(Task<IEnumerable<CatalogType>>), searchMethod!.ReturnType);
    }

    [Fact]
    public void ICatalogTypeService_GenericConstraintsAreCorrect()
    {
        // Arrange
        var serviceType = typeof(ICatalogTypeService);

        // Act
        var getAllMethod = serviceType.GetMethod("GetAllAsync");
        var getByIdMethod = serviceType.GetMethod("GetByIdAsync");
        var createMethod = serviceType.GetMethod("CreateAsync");
        var updateMethod = serviceType.GetMethod("UpdateAsync");
        var searchMethod = serviceType.GetMethod("SearchAsync");

        // Assert - All methods work with CatalogType entity
        Assert.True(getAllMethod!.ReturnType.GetGenericArguments().Any(t =>
            t.GetGenericArguments().Contains(typeof(CatalogType))));

        Assert.Equal(typeof(CatalogType),
            getByIdMethod!.ReturnType.GetGenericArguments()[0].GetGenericArguments()[0]);

        Assert.Equal(typeof(CatalogType),
            createMethod!.GetParameters()[0].ParameterType);

        Assert.Equal(typeof(CatalogType),
            updateMethod!.GetParameters()[1].ParameterType);

        Assert.True(searchMethod!.ReturnType.GetGenericArguments().Any(t =>
            t.GetGenericArguments().Contains(typeof(CatalogType))));
    }
}