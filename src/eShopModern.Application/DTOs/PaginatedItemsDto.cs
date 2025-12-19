namespace eShopModern.Application.DTOs;

/// <summary>
/// Data transfer object for paginated items
/// </summary>
/// <typeparam name="T">The type of items in the paginated result</typeparam>
public class PaginatedItemsDto<T> where T : class
{
    /// <summary>
    /// Gets or sets the page index (zero-based)
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total count of items
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets the total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Gets or sets the items in the current page
    /// </summary>
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

    /// <summary>
    /// Gets whether there is a previous page
    /// </summary>
    public bool HasPrevious => PageIndex > 0;

    /// <summary>
    /// Gets whether there is a next page
    /// </summary>
    public bool HasNext => PageIndex < (TotalPages - 1);
}