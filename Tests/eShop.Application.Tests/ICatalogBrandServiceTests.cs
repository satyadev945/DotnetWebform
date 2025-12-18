using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;

namespace Tests.eShop.Domain.Interfaces.Services;

public class ICatalogBrandServiceTests
{
    [Fact]
    public void ICatalogBrandService_HasGetAllAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogBrandService.GetAllAsync));

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
    public void ICatalogBrandService_HasGetByIdAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogBrandService.GetByIdAsync));

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
    public void ICatalogBrandService_HasCreateAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogBrandService.CreateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogBrand>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogBrand), parameters[0].ParameterType);
        Assert.Equal("brand", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogBrandService_HasUpdateAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogBrandService.UpdateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(3, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);
        Assert.Equal(typeof(CatalogBrand), parameters[1].ParameterType);
        Assert.Equal("brand", parameters[1].Name);
        Assert.Equal(typeof(CancellationToken), parameters[2].ParameterType);
        Assert.True(parameters[2].HasDefaultValue);
    }

    [Fact]
    public void ICatalogBrandService_HasDeleteAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogBrandService.DeleteAsync));

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
    public void ICatalogBrandService_HasSearchAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogBrandService.SearchAsync));

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
    public void ICatalogBrandService_IsInterface()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act & Assert
        Assert.True(serviceType.IsInterface);
        Assert.True(serviceType.IsPublic);
    }

    [Fact]
    public void ICatalogBrandService_HasCorrectMethodCount()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var methods = serviceType.GetMethods();

        // Assert - Should have exactly 6 methods
        Assert.Equal(6, methods.Length);
    }

    [Fact]
    public void ICatalogBrandService_AllMethodsAreAsync()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);
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
    public void ICatalogBrandService_AllMethodsHaveCancellationToken()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);
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
    public void ICatalogBrandService_IsInCorrectNamespace()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act & Assert
        Assert.Equal("eShop.Domain.Interfaces.Services", serviceType.Namespace);
    }

    [Fact]
    public void ICatalogBrandService_HasBusinessOperations()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

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
    public void ICatalogBrandService_HasExpectedMethod(string methodName)
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var method = serviceType.GetMethod(methodName);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.Name.EndsWith("Async"));
    }

    [Fact]
    public void ICatalogBrandService_UpdateMethodTakesIdAndBrand()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var updateMethod = serviceType.GetMethod("UpdateAsync");

        // Assert
        Assert.NotNull(updateMethod);
        var parameters = updateMethod!.GetParameters();
        Assert.Equal(3, parameters.Length); // id, brand, cancellationToken

        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);

        Assert.Equal(typeof(CatalogBrand), parameters[1].ParameterType);
        Assert.Equal("brand", parameters[1].Name);
    }

    [Fact]
    public void ICatalogBrandService_CreateMethodReturnsCreatedBrand()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var createMethod = serviceType.GetMethod("CreateAsync");

        // Assert
        Assert.NotNull(createMethod);
        Assert.Equal(typeof(Task<CatalogBrand>), createMethod!.ReturnType);
    }

    [Fact]
    public void ICatalogBrandService_SearchAcceptsStringParameter()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var searchMethod = serviceType.GetMethod("SearchAsync");

        // Assert
        Assert.NotNull(searchMethod);
        var parameters = searchMethod!.GetParameters();
        Assert.Equal(typeof(string), parameters[0].ParameterType);
        Assert.Equal("searchTerm", parameters[0].Name);
    }

    [Fact]
    public void ICatalogBrandService_MethodsHaveCorrectSignatures()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

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
    public void ICatalogBrandService_CreateMethodParameterNaming()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var createMethod = serviceType.GetMethod("CreateAsync");

        // Assert
        Assert.NotNull(createMethod);
        var parameters = createMethod!.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal("brand", parameters[0].Name);
        Assert.Equal(typeof(CatalogBrand), parameters[0].ParameterType);
    }

    [Fact]
    public void ICatalogBrandService_AllCRUDOperationsReturnCorrectTypes()
    {
        // Arrange
        var serviceType = typeof(ICatalogBrandService);

        // Act
        var createMethod = serviceType.GetMethod("CreateAsync");
        var getAllMethod = serviceType.GetMethod("GetAllAsync");
        var getByIdMethod = serviceType.GetMethod("GetByIdAsync");
        var updateMethod = serviceType.GetMethod("UpdateAsync");
        var deleteMethod = serviceType.GetMethod("DeleteAsync");
        var searchMethod = serviceType.GetMethod("SearchAsync");

        // Assert
        Assert.Equal(typeof(Task<CatalogBrand>), createMethod!.ReturnType);
        Assert.Equal(typeof(Task<IEnumerable<CatalogBrand>>), getAllMethod!.ReturnType);
        Assert.Equal(typeof(Task<CatalogBrand?>), getByIdMethod!.ReturnType);
        Assert.Equal(typeof(Task), updateMethod!.ReturnType);
        Assert.Equal(typeof(Task), deleteMethod!.ReturnType);
        Assert.Equal(typeof(Task<IEnumerable<CatalogBrand>>), searchMethod!.ReturnType);
    }
}