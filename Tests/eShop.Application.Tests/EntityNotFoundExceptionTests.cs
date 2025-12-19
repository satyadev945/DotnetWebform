using Xunit;
using System;
using eShop.Domain.Exceptions;

namespace Tests.eShop.Domain.Exceptions;

public class EntityNotFoundExceptionTests
{
    [Fact]
    public void Constructor_WithEntityNameAndId_SetsPropertiesCorrectly()
    {
        // Arrange
        var entityName = "CatalogItem";
        var entityId = 123;

        // Act
        var exception = new EntityNotFoundException(entityName, entityId);

        // Assert
        Assert.Equal(entityName, exception.EntityName);
        Assert.Equal(entityId, exception.EntityId);
    }

    [Fact]
    public void Constructor_WithEntityNameAndId_SetsMessageCorrectly()
    {
        // Arrange
        var entityName = "CatalogItem";
        var entityId = 123;
        var expectedMessage = "CatalogItem with ID 123 was not found.";

        // Act
        var exception = new EntityNotFoundException(entityName, entityId);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData("CatalogItem", 1)]
    [InlineData("CatalogBrand", 42)]
    [InlineData("CatalogType", 999)]
    [InlineData("User", 0)]
    public void Constructor_WithDifferentEntityNamesAndIds_SetsPropertiesCorrectly(string entityName, int entityId)
    {
        // Arrange & Act
        var exception = new EntityNotFoundException(entityName, entityId);

        // Assert
        Assert.Equal(entityName, exception.EntityName);
        Assert.Equal(entityId, exception.EntityId);
        Assert.Contains(entityName, exception.Message);
        Assert.Contains(entityId.ToString(), exception.Message);
    }

    [Fact]
    public void Constructor_WithEmptyEntityName_SetsPropertiesCorrectly()
    {
        // Arrange
        var entityName = "";
        var entityId = 123;

        // Act
        var exception = new EntityNotFoundException(entityName, entityId);

        // Assert
        Assert.Equal(entityName, exception.EntityName);
        Assert.Equal(entityId, exception.EntityId);
        Assert.Equal(" with ID 123 was not found.", exception.Message);
    }

    [Fact]
    public void Constructor_WithNullEntityName_SetsPropertiesCorrectly()
    {
        // Arrange
        string entityName = null!;
        var entityId = 123;

        // Act
        var exception = new EntityNotFoundException(entityName, entityId);

        // Assert
        Assert.Null(exception.EntityName);
        Assert.Equal(entityId, exception.EntityId);
        Assert.Contains("with ID 123 was not found.", exception.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void Constructor_WithBoundaryValues_SetsEntityIdCorrectly(int entityId)
    {
        // Arrange
        var entityName = "TestEntity";

        // Act
        var exception = new EntityNotFoundException(entityName, entityId);

        // Assert
        Assert.Equal(entityId, exception.EntityId);
        Assert.Contains(entityId.ToString(), exception.Message);
    }

    [Fact]
    public void EntityNotFoundException_InheritsFromException()
    {
        // Arrange
        var entityName = "CatalogItem";
        var entityId = 123;

        // Act
        var exception = new EntityNotFoundException(entityName, entityId);

        // Assert
        Assert.IsAssignableFrom<Exception>(exception);
        Assert.True(exception is Exception);
    }

    [Fact]
    public void EntityName_PropertyIsReadOnly()
    {
        // Arrange
        var entityName = "CatalogItem";
        var entityId = 123;
        var exception = new EntityNotFoundException(entityName, entityId);

        // Act & Assert
        var property = typeof(EntityNotFoundException).GetProperty(nameof(EntityNotFoundException.EntityName));
        Assert.NotNull(property);
        Assert.True(property!.CanRead);
        Assert.False(property.CanWrite);
    }

    [Fact]
    public void EntityId_PropertyIsReadOnly()
    {
        // Arrange
        var entityName = "CatalogItem";
        var entityId = 123;
        var exception = new EntityNotFoundException(entityName, entityId);

        // Act & Assert
        var property = typeof(EntityNotFoundException).GetProperty(nameof(EntityNotFoundException.EntityId));
        Assert.NotNull(property);
        Assert.True(property!.CanRead);
        Assert.False(property.CanWrite);
    }

    [Fact]
    public void Constructor_OnlyAcceptsTwoParameters()
    {
        // Arrange & Act
        var constructors = typeof(EntityNotFoundException).GetConstructors();

        // Assert
        Assert.Single(constructors);
        var constructor = constructors[0];
        var parameters = constructor.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(string), parameters[0].ParameterType);
        Assert.Equal(typeof(int), parameters[1].ParameterType);
    }

    [Fact]
    public void Exception_CanBeThrown()
    {
        // Arrange
        var entityName = "CatalogItem";
        var entityId = 123;

        // Act & Assert
        var exception = Assert.Throws<EntityNotFoundException>((Action)(() =>
        {
            throw new EntityNotFoundException(entityName, entityId);
        }));

        Assert.Equal(entityName, exception.EntityName);
        Assert.Equal(entityId, exception.EntityId);
    }

    [Fact]
    public void Exception_CanBeCaught()
    {
        // Arrange
        var entityName = "CatalogItem";
        var entityId = 123;
        EntityNotFoundException caughtException = null!;

        // Act
        try
        {
            throw new EntityNotFoundException(entityName, entityId);
        }
        catch (EntityNotFoundException ex)
        {
            caughtException = ex;
        }

        // Assert
        Assert.NotNull(caughtException);
        Assert.Equal(entityName, caughtException.EntityName);
        Assert.Equal(entityId, caughtException.EntityId);
    }

    [Fact]
    public void Exception_CanBeCaughtAsBaseException()
    {
        // Arrange
        var entityName = "CatalogItem";
        var entityId = 123;
        Exception caughtException = null!;

        // Act
        try
        {
            throw new EntityNotFoundException(entityName, entityId);
        }
        catch (Exception ex)
        {
            caughtException = ex;
        }

        // Assert
        Assert.NotNull(caughtException);
        Assert.IsType<EntityNotFoundException>(caughtException);
        var specificException = (EntityNotFoundException)caughtException;
        Assert.Equal(entityName, specificException.EntityName);
        Assert.Equal(entityId, specificException.EntityId);
    }

    [Fact]
    public void Exception_IsInCorrectNamespace()
    {
        // Arrange
        var exceptionType = typeof(EntityNotFoundException);

        // Act & Assert
        Assert.Equal("eShop.Domain.Exceptions", exceptionType.Namespace);
    }

    [Theory]
    [InlineData("Product", 1, "Product with ID 1 was not found.")]
    [InlineData("Order", 42, "Order with ID 42 was not found.")]
    [InlineData("Customer", 999, "Customer with ID 999 was not found.")]
    public void Constructor_MessageFormat_IsConsistent(string entityName, int entityId, string expectedMessage)
    {
        // Arrange & Act
        var exception = new EntityNotFoundException(entityName, entityId);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }
}