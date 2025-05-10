using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.domain.validator;
using Microsoft.EntityFrameworkCore;

namespace Internship.NetSiemens2025.persistence.database.implementation;

/// <summary>
/// Repository for storing books.
/// </summary>
/// <inheritdoc cref="DbRepository{TId,TEntity}"/>
public class BorrowingDbRepository : DbRepository<int, Borrowing>, IBorrowingRepository
{
    /// <summary>
    /// Constructor for the BorrowingDbRepository class.
    /// Initializes a repository for handling Borrowing entities by inheriting from the base repository.
    /// </summary>
    /// <param name="validator">
    /// Instance of IValidator&lt;Borrowing&gt;, used to validate Borrowing entities before they are stored.
    /// </param>
    /// <param name="context">
    /// Instance of LibraryDbContext, used for interacting with the database.
    /// </param>
    public BorrowingDbRepository(IValidator<Borrowing> validator, LibraryDbContext context) : base(validator, context)
    {
    }
    
    public override List<Borrowing> GetAll()
    {
        return Context.Borrowings
            .Include(b => b.Reader)
            .Include(b => b.BorrowingItems)
                .ThenInclude(bi => bi.Book)
            .ToList();
    }

    public List<Borrowing> FilterReturnedBorrowing(bool returned = false)
    {
        return Context.Borrowings
            .Include(b => b.Reader)
            .Include(b => b.BorrowingItems)
                .ThenInclude(bi => bi.Book)
            .Where(bi => returned ? bi.ReturningDate != null: bi.ReturningDate == null)
            .ToList();
    }

    public List<Borrowing> UnreturnedBorrowingsExceededDeadline()
    {
        DateTime deadline = DateTime.Today.AddDays(-21);
        
        return Context.Borrowings
            .Include(b => b.Reader)
            .Include(b => b.BorrowingItems)
                .ThenInclude(bi => bi.Book)
            .Where(bi => bi.ReturningDate == null && bi.BorrowingDate <= deadline)
            .ToList();
    }

    public List<Book> GetMostBorrowedBooksCurrentYear()
    {
        var currentYear = DateTime.Today.Year;
        
        return Context.Borrowings
            .Where(b => b.BorrowingDate.Year == currentYear)
            .SelectMany(b => b.BorrowingItems)
            .GroupBy(bi => bi.Book)
            .OrderByDescending(group => group.Count())
            .Select(group => group.Key)
            .ToList();
    }
}