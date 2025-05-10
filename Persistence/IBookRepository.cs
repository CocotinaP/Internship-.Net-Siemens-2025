using Internship.NetSiemens2025.domain;

namespace Internship.NetSiemens2025.persistence;

/// <summary>
/// Repository interface for managing Book entities.
/// Provides data access operations specific to Book objects.
/// </summary>
/// <inheritdoc cref="IRepository{TId,TEntity}"/>
public interface IBookRepository : IRepository<int, Book>
{
    /// <summary>
    /// Filters books based on specified criteria such as titles, authors, genres, or categories.
    /// </summary>
    /// <param name="titles">
    /// Optional list of book titles to filter by.
    /// If null, no filtering is applied based on titles.
    /// </param>
    /// <param name="authors">
    /// Optional list of authors to filter books by.
    /// If null, no filtering is applied based on authors.
    /// </param>
    /// <param name="genres">
    /// Optional list of genres to filter books by.
    /// If null, no filtering is applied based on genres.
    /// </param>
    /// <param name="category">
    /// Optional list of categories to filter books by.
    /// If null, no filtering is applied based on categories.
    /// </param>
    /// <returns>
    /// A list of books that match the specified criteria.
    /// If no criteria are provided, returns all available books.
    /// </returns>
    public List<Book> FilterBooks(List<string>? titles = null,
        List<string>? authors = null,
        List<string>? genres = null,
        List<string>? category = null);
}