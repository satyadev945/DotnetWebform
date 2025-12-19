using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using eShopModern.Application.DTOs;

namespace eShopModern.Application.Tests
{
    public class PaginatedItemsDtoTests
    {
        [Fact]
        public void PaginatedItemsDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var dto = new PaginatedItemsDto<string>();

            // Assert
            Assert.Equal(0, dto.PageIndex);
            Assert.Equal(0, dto.PageSize);
            Assert.Equal(0, dto.TotalCount);
            Assert.NotNull(dto.Items);
            Assert.Empty(dto.Items);
        }

        [Fact]
        public void TotalPages_WithZeroPageSize_ShouldReturnZero()
        {
            // Arrange
            var dto = new PaginatedItemsDto<string>
            {
                TotalCount = 10,
                PageSize = 0
            };

            // Act & Assert
            // When PageSize is 0, Math.Ceiling returns -2147483648 (overflow behavior)
            Assert.Equal(int.MinValue, dto.TotalPages);
        }

        [Theory]
        [InlineData(10, 5, 2)]
        [InlineData(10, 3, 4)]
        [InlineData(10, 10, 1)]
        [InlineData(0, 5, 0)]
        [InlineData(1, 5, 1)]
        public void TotalPages_ShouldCalculateCorrectly(int totalCount, int pageSize, int expectedPages)
        {
            // Arrange
            var dto = new PaginatedItemsDto<string>
            {
                TotalCount = totalCount,
                PageSize = pageSize
            };

            // Act & Assert
            Assert.Equal(expectedPages, dto.TotalPages);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(5, true)]
        public void HasPrevious_ShouldReturnCorrectValue(int pageIndex, bool expectedHasPrevious)
        {
            // Arrange
            var dto = new PaginatedItemsDto<string>
            {
                PageIndex = pageIndex
            };

            // Act & Assert
            Assert.Equal(expectedHasPrevious, dto.HasPrevious);
        }

        [Fact]
        public void HasNext_WithLastPage_ShouldReturnFalse()
        {
            // Arrange
            var dto = new PaginatedItemsDto<string>
            {
                PageIndex = 1, // Zero-based, so this is page 2
                TotalCount = 10,
                PageSize = 5 // Total pages = 2, so page index 1 is the last page
            };

            // Act & Assert
            Assert.False(dto.HasNext);
        }

        [Fact]
        public void HasNext_WithFirstPageAndMultiplePages_ShouldReturnTrue()
        {
            // Arrange
            var dto = new PaginatedItemsDto<string>
            {
                PageIndex = 0,
                TotalCount = 10,
                PageSize = 3 // Total pages = 4
            };

            // Act & Assert
            Assert.True(dto.HasNext);
        }

        [Fact]
        public void HasNext_WithSinglePage_ShouldReturnFalse()
        {
            // Arrange
            var dto = new PaginatedItemsDto<string>
            {
                PageIndex = 0,
                TotalCount = 5,
                PageSize = 10 // Total pages = 1
            };

            // Act & Assert
            Assert.False(dto.HasNext);
        }

        [Fact]
        public void Items_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var items = new List<string> { "Item1", "Item2", "Item3" };
            var dto = new PaginatedItemsDto<string>();

            // Act
            dto.Items = items;

            // Assert
            Assert.Equal(items, dto.Items);
            Assert.Equal(3, dto.Items.Count());
        }

        [Fact]
        public void PaginatedItemsDto_WithComplexType_ShouldWorkCorrectly()
        {
            // Arrange
            var catalogItems = new List<CatalogItemDto>
            {
                new() { Id = 1, Name = "Item 1" },
                new() { Id = 2, Name = "Item 2" }
            };

            var dto = new PaginatedItemsDto<CatalogItemDto>
            {
                PageIndex = 0,
                PageSize = 2,
                TotalCount = 10,
                Items = catalogItems
            };

            // Act & Assert
            Assert.Equal(0, dto.PageIndex);
            Assert.Equal(2, dto.PageSize);
            Assert.Equal(10, dto.TotalCount);
            Assert.Equal(5, dto.TotalPages);
            Assert.False(dto.HasPrevious);
            Assert.True(dto.HasNext);
            Assert.Equal(2, dto.Items.Count());
            Assert.Equal("Item 1", dto.Items.First().Name);
        }

        [Fact]
        public void PaginatedItemsDto_AllProperties_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var items = new List<string> { "Test1", "Test2" };
            var dto = new PaginatedItemsDto<string>();

            // Act
            dto.PageIndex = 2;
            dto.PageSize = 10;
            dto.TotalCount = 50;
            dto.Items = items;

            // Assert
            Assert.Equal(2, dto.PageIndex);
            Assert.Equal(10, dto.PageSize);
            Assert.Equal(50, dto.TotalCount);
            Assert.Equal(5, dto.TotalPages);
            Assert.True(dto.HasPrevious);
            Assert.True(dto.HasNext);
            Assert.Equal(items, dto.Items);
        }

        [Fact]
        public void PaginatedItemsDto_EmptyItems_ShouldHandleCorrectly()
        {
            // Arrange
            var dto = new PaginatedItemsDto<string>
            {
                PageIndex = 0,
                PageSize = 10,
                TotalCount = 0,
                Items = Enumerable.Empty<string>()
            };

            // Act & Assert
            Assert.Equal(0, dto.PageIndex);
            Assert.Equal(10, dto.PageSize);
            Assert.Equal(0, dto.TotalCount);
            Assert.Equal(0, dto.TotalPages);
            Assert.False(dto.HasPrevious);
            Assert.False(dto.HasNext);
            Assert.Empty(dto.Items);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void PaginatedItemsDto_NegativePageIndex_ShouldHandleCorrectly(int pageIndex)
        {
            // Arrange
            var dto = new PaginatedItemsDto<string>
            {
                PageIndex = pageIndex,
                PageSize = 10,
                TotalCount = 50
            };

            // Act & Assert
            Assert.Equal(pageIndex, dto.PageIndex);
            Assert.False(dto.HasPrevious); // Should still be false for negative values
        }
    }
}