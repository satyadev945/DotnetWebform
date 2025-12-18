using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;

namespace Tests.eShop.Domain.Interfaces.Services;

public class ICatalogItemServiceTests
{
    [Fact]
    public void ICatalogItemService_HasGetAllAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogItemService.GetAllAsync));

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
    public void ICatalogItemService_HasGetByIdAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogItemService.GetByIdAsync));

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
    public void ICatalogItemService_HasCreateAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogItemService.CreateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<CatalogItem>), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(CatalogItem), parameters[0].ParameterType);
        Assert.Equal("item", parameters[0].Name);
        Assert.Equal(typeof(CancellationToken), parameters[1].ParameterType);
        Assert.True(parameters[1].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemService_HasUpdateAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogItemService.UpdateAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method!.ReturnType);

        var parameters = method.GetParameters();
        Assert.Equal(3, parameters.Length);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);
        Assert.Equal(typeof(CatalogItem), parameters[1].ParameterType);
        Assert.Equal("item", parameters[1].Name);
        Assert.Equal(typeof(CancellationToken), parameters[2].ParameterType);
        Assert.True(parameters[2].HasDefaultValue);
    }

    [Fact]
    public void ICatalogItemService_HasDeleteAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogItemService.DeleteAsync));

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
    public void ICatalogItemService_HasSearchAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogItemService.SearchAsync));

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
    public void ICatalogItemService_HasGetPagedAsyncMethod()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var method = serviceType.GetMethod(nameof(ICatalogItemService.GetPagedAsync));

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
    public void ICatalogItemService_IsInterface()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act & Assert
        Assert.True(serviceType.IsInterface);
        Assert.True(serviceType.IsPublic);
    }

    [Fact]
    public void ICatalogItemService_HasCorrectMethodCount()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var methods = serviceType.GetMethods();

        // Assert - Should have exactly 7 methods
        Assert.Equal(7, methods.Length);
    }

    [Fact]
    public void ICatalogItemService_AllMethodsAreAsync()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);
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
    public void ICatalogItemService_AllMethodsHaveCancellationToken()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);
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
    public void ICatalogItemService_IsInCorrectNamespace()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act & Assert
        Assert.Equal("eShop.Domain.Interfaces.Services", serviceType.Namespace);
    }

    [Fact]
    public void ICatalogItemService_HasBusinessOperations()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var hasCreate = serviceType.GetMethod("CreateAsync") != null;
        var hasRead = serviceType.GetMethod("GetAllAsync") != null &&
                      serviceType.GetMethod("GetByIdAsync") != null;
        var hasUpdate = serviceType.GetMethod("UpdateAsync") != null;
        var hasDelete = serviceType.GetMethod("DeleteAsync") != null;
        var hasSearch = serviceType.GetMethod("SearchAsync") != null;
        var hasPaging = serviceType.GetMethod("GetPagedAsync") != null;

        // Assert
        Assert.True(hasCreate, "Service should have Create operation (CreateAsync)");
        Assert.True(hasRead, "Service should have Read operations (GetAllAsync, GetByIdAsync)");
        Assert.True(hasUpdate, "Service should have Update operation (UpdateAsync)");
        Assert.True(hasDelete, "Service should have Delete operation (DeleteAsync)");
        Assert.True(hasSearch, "Service should have Search capability (SearchAsync)");
        Assert.True(hasPaging, "Service should have Paging capability (GetPagedAsync)");
    }

    [Theory]
    [InlineData("GetAllAsync")]
    [InlineData("GetByIdAsync")]
    [InlineData("CreateAsync")]
    [InlineData("UpdateAsync")]
    [InlineData("DeleteAsync")]
    [InlineData("SearchAsync")]
    [InlineData("GetPagedAsync")]
    public void ICatalogItemService_HasExpectedMethod(string methodName)
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var method = serviceType.GetMethod(methodName);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.Name.EndsWith("Async"));
    }

    [Fact]
    public void ICatalogItemService_UpdateMethodTakesIdAndItem()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var updateMethod = serviceType.GetMethod("UpdateAsync");

        // Assert
        Assert.NotNull(updateMethod);
        var parameters = updateMethod!.GetParameters();
        Assert.Equal(3, parameters.Length); // id, item, cancellationToken

        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("id", parameters[0].Name);

        Assert.Equal(typeof(CatalogItem), parameters[1].ParameterType);
        Assert.Equal("item", parameters[1].Name);
    }

    [Fact]
    public void ICatalogItemService_CreateMethodReturnsCreatedItem()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var createMethod = serviceType.GetMethod("CreateAsync");

        // Assert
        Assert.NotNull(createMethod);
        Assert.Equal(typeof(Task<CatalogItem>), createMethod!.ReturnType);
    }

    [Fact]
    public void ICatalogItemService_GetPagedAsyncReturnsTuple()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var getPagedMethod = serviceType.GetMethod("GetPagedAsync");

        // Assert
        Assert.NotNull(getPagedMethod);
        var returnType = getPagedMethod!.ReturnType;
        Assert.True(returnType.IsGenericType);

        var genericArguments = returnType.GetGenericArguments();
        Assert.Single(genericArguments);

        var tupleType = genericArguments[0];
        Assert.True(tupleType.IsGenericType);
        Assert.Equal("ValueTuple`2", tupleType.Name);
    }

    [Fact]
    public void ICatalogItemService_SearchAcceptsStringParameter()
    {
        // Arrange
        var serviceType = typeof(ICatalogItemService);

        // Act
        var searchMethod = serviceType.GetMethod("SearchAsync");

        // Assert
        Assert.NotNull(searchMethod);
        var parameters = searchMethod!.GetParameters();
        Assert.Equal(typeof(string), parameters[0].ParameterType);
        Assert.Equal("searchTerm", parameters[0].Name);
    }
}