using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.domain.validator;
using Microsoft.EntityFrameworkCore;

namespace Internship.NetSiemens2025.persistence.database.implementation;

/// <summary>
/// Repository for storing borrowing items.
/// </summary>
/// <inheritdoc cref="DbRepository{TId,TEntity}"/>
public class BorrowingItemDbRepository : DbRepository<int, BorrowingItem>, IBorrowingItemRepository
{
    /// <summary>
    /// Constructor for the BorrowingItemDbRepository class.
    /// Initializes a repository for handling BorrowingItem entities by inheriting from the base repository.
    /// </summary>
    /// <param name="validator">
    /// Instance of IValidator&lt;BorrowingItem&gt;, used to validate BorrowingItem entities before they are stored.
    /// </param>
    /// <param name="context">
    /// Instance of LibraryDbContext, used for interacting with the database.
    /// </param>
    public BorrowingItemDbRepository(IValidator<BorrowingItem> validator, LibraryDbContext context) : base(validator, context)
    {
    }
    
    public override List<BorrowingItem> GetAll()
    {
        return Context.BorrowingItems
            .Include(bi => bi.Book)
            .ToList();
    }
}