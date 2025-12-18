using Xunit;
using Moq;
using eShop.Web.Pages;
using eShop.Domain.Interfaces.Services;
using eShop.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.Web.Tests.Pages;

public class IndexModelTests
{
    private readonly Mock<ICatalogService> _catalogServiceMock;
    private readonly Mock<ILogger<IndexModel>> _loggerMock;
    private readonly IndexModel _indexModel;

    public IndexModelTests()
    {
        _catalogServiceMock = new Mock<ICatalogService>();
        _loggerMock = new Mock<ILogger<IndexModel>>();
        _indexModel = new IndexModel(_catalogServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void Constructor_WithNullCatalogService_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new IndexModel(null!, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new IndexModel(_catalogServiceMock.Object, null!));
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange, Act & Assert
        var model = new IndexModel(_catalogServiceMock.Object, _loggerMock.Object);
        Assert.NotNull(model);
    }

    [Fact]
    public void Constructor_ShouldInitializeCatalogItems()
    {
        // Arrange & Act
        var model = new IndexModel(_catalogServiceMock.Object, _loggerMock.Object);

        // Assert
        Assert.NotNull(model.CatalogItems);
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadCatalogItems()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem
            {
                Id = 1,
                Name = "Item 1",
                Price = 10m,
                CatalogTypeId = 1,
                CatalogBrandId = 1,
                CatalogType = new CatalogType { Id = 1, Type = "Type1" },
                CatalogBrand = new CatalogBrand { Id = 1, Brand = "Brand1" }
            },
            new CatalogItem
            {
                Id = 2,
                Name = "Item 2",
                Price = 20m,
                CatalogTypeId = 2,
                CatalogBrandId = 2,
                CatalogType = new CatalogType { Id = 2, Type = "Type2" },
                CatalogBrand = new CatalogBrand { Id = 2, Brand = "Brand2" }
            }
        };

        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(10, 0, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems.AsEnumerable(), 2L));

        // Act
        await _indexModel.OnGetAsync(0, 10);

        // Assert
        Assert.NotNull(_indexModel.CatalogItems);
        Assert.Equal(2, _indexModel.CatalogItems.Items.Count());
        Assert.Equal(2, _indexModel.CatalogItems.TotalItems);
    }

    [Fact]
    public async Task OnGetAsync_WithDefaultParameters_ShouldUseDefaults()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>();
        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(10, 0, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems.AsEnumerable(), 0L));

        // Act
        await _indexModel.OnGetAsync();

        // Assert
        _catalogServiceMock.Verify(x => x.GetCatalogItemsPaginatedAsync(10, 0, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_WithCustomParameters_ShouldUseProvidedValues()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>();
        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(20, 2, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems.AsEnumerable(), 0L));

        // Act
        await _indexModel.OnGetAsync(2, 20);

        // Assert
        _catalogServiceMock.Verify(x => x.GetCatalogItemsPaginatedAsync(20, 2, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ShouldMapCatalogItemsToViewModels()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem
            {
                Id = 1,
                Name = "Test Item",
                Description = "Test Description",
                Price = 99.99m,
                PictureFileName = "test.jpg",
                PictureUri = "https://example.com/test.jpg",
                CatalogTypeId = 1,
                CatalogType = new CatalogType { Id = 1, Type = "Electronics" },
                CatalogBrandId = 2,
                CatalogBrand = new CatalogBrand { Id = 2, Brand = "Nike" },
                AvailableStock = 50,
                RestockThreshold = 5,
                MaxStockThreshold = 200,
                OnReorder = false
            }
        };

        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(10, 0, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems.AsEnumerable(), 1L));

        // Act
        await _indexModel.OnGetAsync(0, 10);

        // Assert
        var viewModel = _indexModel.CatalogItems.Items.First();
        Assert.Equal(1, viewModel.Id);
        Assert.Equal("Test Item", viewModel.Name);
        Assert.Equal("Test Description", viewModel.Description);
        Assert.Equal(99.99m, viewModel.Price);
        Assert.Equal("test.jpg", viewModel.PictureFileName);
        Assert.Equal("https://example.com/test.jpg", viewModel.PictureUri);
        Assert.Equal(1, viewModel.CatalogTypeId);
        Assert.Equal("Electronics", viewModel.CatalogTypeName);
        Assert.Equal(2, viewModel.CatalogBrandId);
        Assert.Equal("Nike", viewModel.CatalogBrandName);
        Assert.Equal(50, viewModel.AvailableStock);
        Assert.Equal(5, viewModel.RestockThreshold);
        Assert.Equal(200, viewModel.MaxStockThreshold);
        Assert.False(viewModel.OnReorder);
    }

    [Fact]
    public async Task OnGetAsync_WhenServiceThrowsException_ShouldHandleGracefully()
    {
        // Arrange
        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        await _indexModel.OnGetAsync(0, 10);

        // Assert
        Assert.NotNull(_indexModel.CatalogItems);
        Assert.Empty(_indexModel.CatalogItems.Items);
        Assert.Equal(0, _indexModel.CatalogItems.TotalItems);
    }

    [Fact]
    public async Task OnGetAsync_WithNullCatalogType_ShouldHandleGracefully()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem
            {
                Id = 1,
                Name = "Item Without Type",
                Price = 10m,
                CatalogTypeId = 1,
                CatalogBrandId = 1,
                CatalogType = null,
                CatalogBrand = new CatalogBrand { Id = 1, Brand = "Brand1" }
            }
        };

        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(10, 0, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems.AsEnumerable(), 1L));

        // Act
        await _indexModel.OnGetAsync(0, 10);

        // Assert
        var viewModel = _indexModel.CatalogItems.Items.First();
        Assert.Null(viewModel.CatalogTypeName);
    }

    [Fact]
    public async Task OnGetAsync_WithNullCatalogBrand_ShouldHandleGracefully()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem
            {
                Id = 1,
                Name = "Item Without Brand",
                Price = 10m,
                CatalogTypeId = 1,
                CatalogBrandId = 1,
                CatalogType = new CatalogType { Id = 1, Type = "Type1" },
                CatalogBrand = null
            }
        };

        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(10, 0, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems.AsEnumerable(), 1L));

        // Act
        await _indexModel.OnGetAsync(0, 10);

        // Assert
        var viewModel = _indexModel.CatalogItems.Items.First();
        Assert.Null(viewModel.CatalogBrandName);
    }

    [Fact]
    public async Task OnGetAsync_ShouldSetCorrectPaginationProperties()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1", Price = 10m, CatalogTypeId = 1, CatalogBrandId = 1 }
        };

        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(20, 3, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems.AsEnumerable(), 100L));

        // Act
        await _indexModel.OnGetAsync(3, 20);

        // Assert
        Assert.Equal(3, _indexModel.CatalogItems.PageIndex);
        Assert.Equal(20, _indexModel.CatalogItems.PageSize);
        Assert.Equal(100, _indexModel.CatalogItems.TotalItems);
    }

    [Fact]
    public async Task OnGetAsync_WithEmptyResult_ShouldReturnEmptyViewModel()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>();
        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(10, 0, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems.AsEnumerable(), 0L));

        // Act
        await _indexModel.OnGetAsync(0, 10);

        // Assert
        Assert.NotNull(_indexModel.CatalogItems);
        Assert.Empty(_indexModel.CatalogItems.Items);
        Assert.Equal(0, _indexModel.CatalogItems.TotalItems);
    }

    [Fact]
    public async Task OnGetAsync_WithCancellationToken_ShouldPassTokenToService()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>();
        var cancellationToken = new CancellationToken();

        _catalogServiceMock
            .Setup(x => x.GetCatalogItemsPaginatedAsync(10, 0, cancellationToken))
            .ReturnsAsync((catalogItems.AsEnumerable(), 0L));

        // Act
        await _indexModel.OnGetAsync(0, 10, cancellationToken);

        // Assert
        _catalogServiceMock.Verify(x => x.GetCatalogItemsPaginatedAsync(10, 0, cancellationToken), Times.Once);
    }
}
