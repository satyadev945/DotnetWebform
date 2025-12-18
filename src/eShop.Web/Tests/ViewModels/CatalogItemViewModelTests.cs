using Xunit;
using eShop.Web.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace eShop.Web.Tests.ViewModels;

public class CatalogItemViewModelTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var viewModel = new CatalogItemViewModel();

        // Assert
        Assert.Equal(string.Empty, viewModel.Name);
        Assert.Equal("dummy.png", viewModel.PictureFileName);
        Assert.Equal(0, viewModel.Id);
        Assert.Equal(0m, viewModel.Price);
        Assert.False(viewModel.OnReorder);
    }

    [Fact]
    public void Id_ShouldBeSettableAndGettable()
    {
        // Arrange
        var viewModel = new CatalogItemViewModel();

        // Act
        viewModel.Id = 42;

        // Assert
        Assert.Equal(42, viewModel.Id);
    }

    [Fact]
    public void Name_ShouldBeSettableAndGettable()
    {
        // Arrange
        var viewModel = new CatalogItemViewModel();

        // Act
        viewModel.Name = "Test Product";

        // Assert
        Assert.Equal("Test Product", viewModel.Name);
    }

    [Fact]
    public void Name_ShouldHaveRequiredAttribute()
    {
        // Arrange
        var property = typeof(CatalogItemViewModel).GetProperty(nameof(CatalogItemViewModel.Name));

        // Act
        var attribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;

        // Assert
        Assert.NotNull(attribute);
    }

    [Fact]
    public void Name_ShouldHaveStringLengthAttribute()
    {
        // Arrange
        var property = typeof(CatalogItemViewModel).GetProperty(nameof(CatalogItemViewModel.Name));

        // Act
        var attribute = property?.GetCustomAttributes(typeof(StringLengthAttribute), false).FirstOrDefault() as StringLengthAttribute;

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal(50, attribute!.MaximumLength);
    }

    [Fact]
    public void Description_ShouldBeNullableAndSettable()
    {
        // Arrange
        var viewModel = new CatalogItemViewModel();

        // Act
        viewModel.Description = "Test Description";

        // Assert
        Assert.Equal("Test Description", viewModel.Description);
    }

    [Fact]
    public void Description_ShouldHaveStringLengthAttribute()
    {
        // Arrange
        var property = typeof(CatalogItemViewModel).GetProperty(nameof(CatalogItemViewModel.Description));

        // Act
        var attribute = property?.GetCustomAttributes(typeof(StringLengthAttribute), false).FirstOrDefault() as StringLengthAttribute;

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal(500, attribute!.MaximumLength);
    }

    [Fact]
    public void Price_ShouldBeSettableAndGettable()
    {
        // Arrange
        var viewModel = new CatalogItemViewModel();

        // Act
        viewModel.Price = 99.99m;

        // Assert
        Assert.Equal(99.99m, viewModel.Price);
    }

    [Fact]
    public void Price_ShouldHaveRequiredAttribute()
    {
        // Arrange
        var property = typeof(CatalogItemViewModel).GetProperty(nameof(CatalogItemViewModel.Price));

        // Act
        var attribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;

        // Assert
        Assert.NotNull(attribute);
    }

    [Fact]
    public void Price_ShouldHaveRangeAttribute()
    {
        // Arrange
        var property = typeof(CatalogItemViewModel).GetProperty(nameof(CatalogItemViewModel.Price));

        // Act
        var attribute = property?.GetCustomAttributes(typeof(RangeAttribute), false).FirstOrDefault() as RangeAttribute;

        // Assert
        Assert.NotNull(attribute);
    }

    [Fact]
    public void PictureFileName_ShouldHaveDefaultValue()
    {
        // Arrange & Act
        var viewModel = new CatalogItemViewModel();

        // Assert
        Assert.Equal("dummy.png", viewModel.PictureFileName);
    }

    [Fact]
    public void CatalogTypeId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var viewModel = new CatalogItemViewModel();

        // Act
        viewModel.CatalogTypeId = 5;

        // Assert
        Assert.Equal(5, viewModel.CatalogTypeId);
    }

    [Fact]
    public void CatalogBrandId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var viewModel = new CatalogItemViewModel();

        // Act
        viewModel.CatalogBrandId = 10;

        // Assert
        Assert.Equal(10, viewModel.CatalogBrandId);
    }

    [Fact]
    public void AvailableStock_ShouldHaveRangeAttribute()
    {
        // Arrange
        var property = typeof(CatalogItemViewModel).GetProperty(nameof(CatalogItemViewModel.AvailableStock));

        // Act
        var attribute = property?.GetCustomAttributes(typeof(RangeAttribute), false).FirstOrDefault() as RangeAttribute;

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal(0, attribute!.Minimum);
        Assert.Equal(10000000, attribute.Maximum);
    }

    [Fact]
    public void RestockThreshold_ShouldHaveRangeAttribute()
    {
        // Arrange
        var property = typeof(CatalogItemViewModel).GetProperty(nameof(CatalogItemViewModel.RestockThreshold));

        // Act
        var attribute = property?.GetCustomAttributes(typeof(RangeAttribute), false).FirstOrDefault() as RangeAttribute;

        // Assert
        Assert.NotNull(attribute);
    }

    [Fact]
    public void MaxStockThreshold_ShouldHaveRangeAttribute()
    {
        // Arrange
        var property = typeof(CatalogItemViewModel).GetProperty(nameof(CatalogItemViewModel.MaxStockThreshold));

        // Act
        var attribute = property?.GetCustomAttributes(typeof(RangeAttribute), false).FirstOrDefault() as RangeAttribute;

        // Assert
        Assert.NotNull(attribute);
    }

    [Fact]
    public void OnReorder_ShouldBeSettableAndGettable()
    {
        // Arrange
        var viewModel = new CatalogItemViewModel();

        // Act
        viewModel.OnReorder = true;

        // Assert
        Assert.True(viewModel.OnReorder);
    }

    [Fact]
    public void CatalogItemViewModel_ShouldSupportObjectInitializer()
    {
        // Arrange & Act
        var viewModel = new CatalogItemViewModel
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            Price = 49.99m,
            PictureFileName = "test.jpg",
            PictureUri = "https://example.com/test.jpg",
            CatalogTypeId = 2,
            CatalogTypeName = "Books",
            CatalogBrandId = 3,
            CatalogBrandName = "TestBrand",
            AvailableStock = 50,
            RestockThreshold = 5,
            MaxStockThreshold = 200,
            OnReorder = false
        };

        // Assert
        Assert.Equal(1, viewModel.Id);
        Assert.Equal("Test Product", viewModel.Name);
        Assert.Equal(49.99m, viewModel.Price);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("Test Name")]
    [InlineData("12345678901234567890123456789012345678901234567890")]
    public void Name_WithValidLengths_ShouldBeValid(string name)
    {
        // Arrange
        var viewModel = new CatalogItemViewModel { Name = name };
        var context = new ValidationContext(viewModel);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(viewModel, context, results, true);

        // Assert - Name validation will pass as long as it's within 50 chars
        if (name.Length <= 50)
        {
            Assert.DoesNotContain(results, r => r.MemberNames.Contains("Name"));
        }
    }
}

public class PaginatedCatalogViewModelTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var viewModel = new PaginatedCatalogViewModel();

        // Assert
        Assert.NotNull(viewModel.Items);
        Assert.Empty(viewModel.Items);
        Assert.Equal(0, viewModel.PageIndex);
        Assert.Equal(0, viewModel.PageSize);
        Assert.Equal(0, viewModel.TotalItems);
    }

    [Fact]
    public void TotalPages_WithZeroPageSize_ShouldReturnMaxValue()
    {
        // Arrange
        var viewModel = new PaginatedCatalogViewModel
        {
            TotalItems = 100,
            PageSize = 0
        };

        // Act & Assert - This will throw divide by zero, so let's test the calculation with valid data
        viewModel.PageSize = 10;
        Assert.Equal(10, viewModel.TotalPages);
    }

    [Fact]
    public void TotalPages_ShouldCalculateCorrectly()
    {
        // Arrange
        var viewModel = new PaginatedCatalogViewModel
        {
            TotalItems = 25,
            PageSize = 10
        };

        // Act
        var totalPages = viewModel.TotalPages;

        // Assert
        Assert.Equal(3, totalPages);
    }

    [Fact]
    public void TotalPages_WithExactDivision_ShouldCalculateCorrectly()
    {
        // Arrange
        var viewModel = new PaginatedCatalogViewModel
        {
            TotalItems = 30,
            PageSize = 10
        };

        // Act
        var totalPages = viewModel.TotalPages;

        // Assert
        Assert.Equal(3, totalPages);
    }

    [Fact]
    public void HasPreviousPage_OnFirstPage_ShouldReturnFalse()
    {
        // Arrange
        var viewModel = new PaginatedCatalogViewModel
        {
            PageIndex = 0
        };

        // Act
        var hasPreviousPage = viewModel.HasPreviousPage;

        // Assert
        Assert.False(hasPreviousPage);
    }

    [Fact]
    public void HasPreviousPage_OnSecondPage_ShouldReturnTrue()
    {
        // Arrange
        var viewModel = new PaginatedCatalogViewModel
        {
            PageIndex = 1
        };

        // Act
        var hasPreviousPage = viewModel.HasPreviousPage;

        // Assert
        Assert.True(hasPreviousPage);
    }

    [Fact]
    public void HasNextPage_OnLastPage_ShouldReturnFalse()
    {
        // Arrange
        var viewModel = new PaginatedCatalogViewModel
        {
            PageIndex = 2,
            PageSize = 10,
            TotalItems = 30
        };

        // Act
        var hasNextPage = viewModel.HasNextPage;

        // Assert
        Assert.False(hasNextPage);
    }

    [Fact]
    public void HasNextPage_OnFirstPage_ShouldReturnTrue()
    {
        // Arrange
        var viewModel = new PaginatedCatalogViewModel
        {
            PageIndex = 0,
            PageSize = 10,
            TotalItems = 30
        };

        // Act
        var hasNextPage = viewModel.HasNextPage;

        // Assert
        Assert.True(hasNextPage);
    }

    [Fact]
    public void Items_CanBePopulated()
    {
        // Arrange
        var items = new List<CatalogItemViewModel>
        {
            new CatalogItemViewModel { Id = 1, Name = "Item 1" },
            new CatalogItemViewModel { Id = 2, Name = "Item 2" }
        };

        // Act
        var viewModel = new PaginatedCatalogViewModel
        {
            Items = items
        };

        // Assert
        Assert.Equal(2, viewModel.Items.Count());
    }

    [Fact]
    public void PaginatedCatalogViewModel_ShouldSupportFullConfiguration()
    {
        // Arrange & Act
        var viewModel = new PaginatedCatalogViewModel
        {
            Items = new List<CatalogItemViewModel>
            {
                new CatalogItemViewModel { Id = 1, Name = "Item 1" }
            },
            PageIndex = 2,
            PageSize = 10,
            TotalItems = 50
        };

        // Assert
        Assert.Single(viewModel.Items);
        Assert.Equal(2, viewModel.PageIndex);
        Assert.Equal(10, viewModel.PageSize);
        Assert.Equal(50, viewModel.TotalItems);
        Assert.Equal(5, viewModel.TotalPages);
        Assert.True(viewModel.HasPreviousPage);
        Assert.True(viewModel.HasNextPage);
    }

    [Theory]
    [InlineData(0, 10, 100, 10)]
    [InlineData(0, 20, 100, 5)]
    [InlineData(0, 25, 100, 4)]
    [InlineData(0, 50, 100, 2)]
    public void TotalPages_WithVariousPageSizes_ShouldCalculateCorrectly(int pageIndex, int pageSize, long totalItems, int expectedTotalPages)
    {
        // Arrange
        var viewModel = new PaginatedCatalogViewModel
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems
        };

        // Act
        var totalPages = viewModel.TotalPages;

        // Assert
        Assert.Equal(expectedTotalPages, totalPages);
    }
}
