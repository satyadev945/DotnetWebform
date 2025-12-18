using Xunit;
using eShop.Domain.Entities;

namespace eShop.Domain.Tests.Entities;

public class CatalogBrandTests
{
    [Fact]
    public void Constructor_ShouldInitializeBrandAsEmpty()
    {
        // Arrange & Act
        var catalogBrand = new CatalogBrand();

        // Assert
        Assert.NotNull(catalogBrand.Brand);
        Assert.Equal(string.Empty, catalogBrand.Brand);
    }

    [Fact]
    public void Id_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var expectedId = 42;

        // Act
        catalogBrand.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, catalogBrand.Id);
    }

    [Fact]
    public void Id_ShouldSupportZeroValue()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.Id = 0;

        // Assert
        Assert.Equal(0, catalogBrand.Id);
    }

    [Fact]
    public void Id_ShouldSupportNegativeValue()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.Id = -1;

        // Assert
        Assert.Equal(-1, catalogBrand.Id);
    }

    [Fact]
    public void Brand_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var expectedBrand = "Nike";

        // Act
        catalogBrand.Brand = expectedBrand;

        // Assert
        Assert.Equal(expectedBrand, catalogBrand.Brand);
    }

    [Fact]
    public void Brand_ShouldSupportEmptyString()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.Brand = string.Empty;

        // Assert
        Assert.Equal(string.Empty, catalogBrand.Brand);
    }

    [Fact]
    public void Brand_ShouldSupportLongStrings()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var longBrandName = new string('A', 1000);

        // Act
        catalogBrand.Brand = longBrandName;

        // Assert
        Assert.Equal(longBrandName, catalogBrand.Brand);
        Assert.Equal(1000, catalogBrand.Brand.Length);
    }

    [Fact]
    public void CatalogBrand_ShouldSupportAllPropertiesSet()
    {
        // Arrange & Act
        var catalogBrand = new CatalogBrand
        {
            Id = 5,
            Brand = "Adidas"
        };

        // Assert
        Assert.Equal(5, catalogBrand.Id);
        Assert.Equal("Adidas", catalogBrand.Brand);
    }

    [Fact]
    public void CatalogBrand_ShouldSupportObjectInitializer()
    {
        // Arrange & Act
        var catalogBrand = new CatalogBrand
        {
            Id = 10,
            Brand = "Puma"
        };

        // Assert
        Assert.NotNull(catalogBrand);
        Assert.Equal(10, catalogBrand.Id);
        Assert.Equal("Puma", catalogBrand.Brand);
    }

    [Fact]
    public void Brand_ShouldSupportSpecialCharacters()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var brandWithSpecialChars = "Nike™ & Co. @2024";

        // Act
        catalogBrand.Brand = brandWithSpecialChars;

        // Assert
        Assert.Equal(brandWithSpecialChars, catalogBrand.Brand);
    }

    [Fact]
    public void Brand_ShouldSupportUnicodeCharacters()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var unicodeBrand = "ナイキ";

        // Act
        catalogBrand.Brand = unicodeBrand;

        // Assert
        Assert.Equal(unicodeBrand, catalogBrand.Brand);
    }

    [Fact]
    public void Constructor_ShouldNotThrowException()
    {
        // Arrange & Act
        var exception = Record.Exception(() => new CatalogBrand());

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Id_ShouldDefaultToZero()
    {
        // Arrange & Act
        var catalogBrand = new CatalogBrand();

        // Assert
        Assert.Equal(0, catalogBrand.Id);
    }
}
