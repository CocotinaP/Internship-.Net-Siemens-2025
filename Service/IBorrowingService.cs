using Internship.NetSiemens2025.domain;

namespace Internship.NetSiemens2025.service;

/// <summary>
/// Interface for managing borrowings.
/// Inherits from ICRUDService and provides standard CRUD operations.
/// </summary>
/// <inheritdoc cref="ICRUDService{TId,TEntity}"/>
public interface IBorrowingService : ICRUDService<int, Borrowing>
{
    /// <summary>
    /// Processes the return of a borrowed item.
    /// Updates the borrowing record to reflect that the item has been returned.
    /// </summary>
    /// <param name="borrowing">
    /// The borrowing record associated with the item being returned.
    /// </param>
    public void Return(Borrowing borrowing);

    /// <summary>
    /// FUNCTIONALITY PROPOSED BY ME.
    /// Filters borrowings based on their return status.
    /// If 'returned' is 'true', only borrowings that have been returned will be included.
    /// If 'returned' is 'false', only borrowings that have not been returned will be included.
    /// </summary>
    /// <param name="returned">
    ///     Indicates whether to return only borrowings that have been completed ('true') 
    ///     or only borrowings that are still active ('false').
    ///     The default value is `false`, meaning the method will return borrowings that have NOT been returned.
    /// </param>
    /// <returns>
    /// A list of borrowings filtered according to the return status.
    /// </returns>
    public List<Borrowing> FilterReturnedBorrowing(bool returned = false);
    
    /// <summary>
    /// FUNCTIONALITY PROPOSED BY ME.
    /// Returns a list of borrowings that have exceeded the return deadline and have not been returned.
    /// </summary>
    /// <returns>
    /// A list of borrowings that are overdue and remain unreturned.
    /// </returns>
    public List<Borrowing> UnreturnedBorrowingsExceededDeadline();
    
    /// <summary>
    /// FUNCTIONALITY PROPOSED BY ME.
    /// Retrieves a list of books ordered in descending order based on how many times they were borrowed in the current year.
    /// </summary>
    /// <returns>
    /// A list of books, sorted by the number of times they have been borrowed within the current year.
    /// The most frequently borrowed book appears first.
    /// </returns>
    public List<Book> GetMostBorrowedBooksCurrentYear();
}