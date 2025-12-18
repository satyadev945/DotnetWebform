using Xunit;
using eShop.Domain.Entities;

namespace eShop.Domain.Tests.Entities;

public class CatalogTypeTests
{
    [Fact]
    public void Constructor_ShouldInitializeTypeAsEmpty()
    {
        // Arrange & Act
        var catalogType = new CatalogType();

        // Assert
        Assert.NotNull(catalogType.Type);
        Assert.Equal(string.Empty, catalogType.Type);
    }

    [Fact]
    public void Id_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogType = new CatalogType();
        var expectedId = 123;

        // Act
        catalogType.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, catalogType.Id);
    }

    [Fact]
    public void Id_ShouldSupportZeroValue()
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.Id = 0;

        // Assert
        Assert.Equal(0, catalogType.Id);
    }

    [Fact]
    public void Id_ShouldSupportNegativeValue()
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.Id = -1;

        // Assert
        Assert.Equal(-1, catalogType.Id);
    }

    [Fact]
    public void Type_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogType = new CatalogType();
        var expectedType = "Electronics";

        // Act
        catalogType.Type = expectedType;

        // Assert
        Assert.Equal(expectedType, catalogType.Type);
    }

    [Fact]
    public void Type_ShouldSupportEmptyString()
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.Type = string.Empty;

        // Assert
        Assert.Equal(string.Empty, catalogType.Type);
    }

    [Fact]
    public void Type_ShouldSupportLongStrings()
    {
        // Arrange
        var catalogType = new CatalogType();
        var longTypeName = new string('X', 500);

        // Act
        catalogType.Type = longTypeName;

        // Assert
        Assert.Equal(longTypeName, catalogType.Type);
        Assert.Equal(500, catalogType.Type.Length);
    }

    [Fact]
    public void CatalogType_ShouldSupportAllPropertiesSet()
    {
        // Arrange & Act
        var catalogType = new CatalogType
        {
            Id = 7,
            Type = "Clothing"
        };

        // Assert
        Assert.Equal(7, catalogType.Id);
        Assert.Equal("Clothing", catalogType.Type);
    }

    [Fact]
    public void CatalogType_ShouldSupportObjectInitializer()
    {
        // Arrange & Act
        var catalogType = new CatalogType
        {
            Id = 15,
            Type = "Footwear"
        };

        // Assert
        Assert.NotNull(catalogType);
        Assert.Equal(15, catalogType.Id);
        Assert.Equal("Footwear", catalogType.Type);
    }

    [Fact]
    public void Type_ShouldSupportSpecialCharacters()
    {
        // Arrange
        var catalogType = new CatalogType();
        var typeWithSpecialChars = "Home & Garden™";

        // Act
        catalogType.Type = typeWithSpecialChars;

        // Assert
        Assert.Equal(typeWithSpecialChars, catalogType.Type);
    }

    [Fact]
    public void Type_ShouldSupportUnicodeCharacters()
    {
        // Arrange
        var catalogType = new CatalogType();
        var unicodeType = "家電製品";

        // Act
        catalogType.Type = unicodeType;

        // Assert
        Assert.Equal(unicodeType, catalogType.Type);
    }

    [Fact]
    public void Constructor_ShouldNotThrowException()
    {
        // Arrange & Act
        var exception = Record.Exception(() => new CatalogType());

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Id_ShouldDefaultToZero()
    {
        // Arrange & Act
        var catalogType = new CatalogType();

        // Assert
        Assert.Equal(0, catalogType.Id);
    }

    [Fact]
    public void Type_ShouldSupportMultipleWordsWithSpaces()
    {
        // Arrange
        var catalogType = new CatalogType();
        var multiWordType = "Outdoor Sports Equipment";

        // Act
        catalogType.Type = multiWordType;

        // Assert
        Assert.Equal(multiWordType, catalogType.Type);
    }

    [Fact]
    public void CatalogType_ShouldAllowIdAndTypeToBeModifiedAfterCreation()
    {
        // Arrange
        var catalogType = new CatalogType
        {
            Id = 1,
            Type = "Books"
        };

        // Act
        catalogType.Id = 2;
        catalogType.Type = "Magazines";

        // Assert
        Assert.Equal(2, catalogType.Id);
        Assert.Equal("Magazines", catalogType.Type);
    }
}
