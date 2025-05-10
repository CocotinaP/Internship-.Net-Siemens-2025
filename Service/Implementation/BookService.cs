using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.persistence;

namespace Internship.NetSiemens2025.service.implementation;

/// <summary>
/// Service class for managing Book entities.
/// Inherits generic CRUD operations from CRUDService and implements additional book-specific logic.
/// </summary>
/// <inheritdoc cref="CRUDService{TId,TEntity}"/>
public class BookService : CRUDService<int, Book>, IBookService
{
    /// <summary>
    /// Initializes a new instance of the BookService class.
    /// </summary>
    /// <param name="repository">
    /// Instance of IBookRepository responsible for handling database interactions for Book entities.
    /// </param>
    public BookService(IBookRepository repository) : base(repository)
    {
    }

    public List<Book> FilterBooks(List<string>? titles = null, List<string>? authors = null, List<string>? genres = null, List<string>? category = null)
    {
        return ((IBookRepository)_repository).FilterBooks(titles, authors, genres, category);
    }
}