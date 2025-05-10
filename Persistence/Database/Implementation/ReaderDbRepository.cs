using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.domain.validator;

namespace Internship.NetSiemens2025.persistence.database.implementation;

/// <summary>
/// Repository for storing books.
/// </summary>
/// <inheritdoc cref="DbRepository{TId,TEntity}"/>
public class ReaderDbRepository : DbRepository<int, Reader>, IReaderRepository
{
    /// <summary>
    /// Constructor for the ReaderDbRepository class.
    /// Initializes a repository for handling Reader entities by inheriting from the base repository.
    /// </summary>
    /// <param name="validator">
    /// Instance of IValidator&lt;Reader&gt;, used to validate Reader entities before they are stored.
    /// </param>
    /// <param name="context">
    /// Instance of LibraryDbContext, used for interacting with the database.
    /// </param>
    public ReaderDbRepository(IValidator<Reader> validator, LibraryDbContext context) : base(validator, context)
    {
    }
}