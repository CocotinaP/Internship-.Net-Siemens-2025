using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.domain.validator;

namespace Internship.NetSiemens2025.persistence.database.implementation;

/// <summary>
/// Repository for storing books.
/// </summary>
/// <inheritdoc cref="DbRepository{TId,TEntity}"/>
public class BookDbRepository : DbRepository<int, Book>,IBookRepository
{
    /// <summary>
    /// Constructor for the BookDbRepository class.
    /// Initializes a repository for handling Book entities by inheriting from the base repository.
    /// </summary>
    /// <param name="validator">
    /// Instance of IValidator&lt;Book&gt;, used to validate Book entities before they are stored.
    /// </param>
    /// <param name="context">
    /// Instance of LibraryDbContext, used for interacting with the database.
    /// </param>
    public BookDbRepository(IValidator<Book> validator, LibraryDbContext context) : base(validator, context)
    {
    }
    
    public List<Book> FilterBooks(List<string>? titles = null, List<string>? authors = null, List<string>? genres = null, List<string>? category = null)
    {
        IQueryable<Book> query = Context.Books;

        if (titles != null && titles.Count != 0)
        {
            query = query.Where(b => titles.Contains(b.Title));
        }

        if (authors != null && authors.Count != 0)
        {
            query = query.Where(b => authors.Contains(b.Author));
        }

        if (genres != null && genres.Count != 0)
        {
            query = query.Where(b => genres.Contains(b.Genre));
        }

        if (category != null && category.Count != 0)
        {
            query = query.Where(b => category.Contains(b.Category));
        }
        
        return query.ToList();
    }
}