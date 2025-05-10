using System.Security;
using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.exception;
using Internship.NetSiemens2025.persistence;

namespace Internship.NetSiemens2025.service.implementation;

/// <summary>
/// Service class for managing Borrowing entities.
/// Inherits generic CRUD operations from CRUDService and implements additional borrowing-specific logic.
/// </summary>
/// <inheritdoc cref="CRUDService{TId,TEntity}"/>
public class BorrowingService : CRUDService<int, Borrowing>, IBorrowingService
{
    private IReaderService _readerService;
    private IBorrowingItemService _borrowingItemService;
    private IBookService _bookService;
    
    /// <summary>
    /// Initializes a new instance of the BorrowingService class.
    /// Manages borrowing operations by coordinating interactions between repositories and services.
    /// </summary>
    /// <param name="repository">
    /// Instance of IBorrowingRepository responsible for handling database interactions for Borrowing entities.
    /// </param>
    /// <param name="readerService">
    /// Instance of IReaderService used to manage reader-related operations.
    /// </param>
    /// <param name="borrowingItemService">
    /// Instance of IBorrowingItemService responsible for handling individual borrowing items.
    /// </param>
    /// <param name="bookService">
    /// Instance of IBookService used to manage book-related operations.
    /// </param>
    public BorrowingService(IBorrowingRepository repository, IReaderService readerService, IBorrowingItemService borrowingItemService, IBookService bookService) : base(repository)
    {
        _readerService = readerService;
        _borrowingItemService = borrowingItemService;
        _bookService = bookService;
    }

    /// <summary>
    /// Ensures that the reader associated with a borrowing transaction exists.
    /// If the reader does not exist in the system, they are added.
    /// </summary>
    /// <param name="borrowing">
    /// The borrowing transaction containing the reader information.
    /// </param>
    private void EnsureReaderExist(Borrowing borrowing)
    {
        var reader = _readerService.GetById(borrowing.Reader.Id);

        if (reader == null)
        {
            _readerService.Add(borrowing.Reader);
        }
    }

    /// <summary>
    /// Ensures that the borrowing item transaction exists.
    /// If the borrowing item does not exist in the system, they are added.
    /// </summary>
    /// <param name="borrowingItem">
    /// The borrowing item.
    /// </param>
    private void EnsureBorrowingItemExist(BorrowingItem borrowingItem)
    {
        if (_borrowingItemService.GetById(borrowingItem.Id) == null)
        {
            _borrowingItemService.Add(borrowingItem);
        }
    }

    /// <summary>
    /// Ensures that the borrowing items associated with a borrowing transaction exist.
    /// If the borrowing items does not exist in the system, they are added.
    /// </summary>
    /// <param name="borrowing">
    /// The borrowing transaction containing the borrowing items information.
    /// </param>
    private void EnsureBorrowingItemsExist(Borrowing borrowing)
    {
        var borrowingItems = borrowing.BorrowingItems;

        borrowingItems.ForEach(EnsureBorrowingItemExist);
    }

    /// <summary>
    /// Verifies whether the requested number of books for borrowing is available.
    /// </summary>
    /// <param name="borrowing">
    /// The borrowing transaction containing the list of borrowing items.
    /// </param>
    /// <exception cref="ServiceException">
    /// Thrown when the requested quantity of books exceeds the available stock.
    /// </exception>
    private void VerifyNumberOfAvailableBooksForBorrowing(Borrowing borrowing)
    {
        foreach (var borrowingItem in borrowing.BorrowingItems)
        {
            if (borrowingItem.Quantity > borrowingItem.Book.AvailablilityNumber)
            {
                throw new ServiceException($"There are only {borrowingItem.Book.AvailablilityNumber} available books with title {borrowingItem.Book.Title}");
            }
        }
    }

    /// <summary>
    /// Updates the availability of books when a borrowing transaction occurs.
    /// Deducts the borrowed quantity from the available stock and updates the book records.
    /// </summary>
    /// <param name="borrowing">
    /// The borrowing transaction containing a list of borrowing items.
    /// </param>
    private void Borrow(Borrowing borrowing)
    {
        foreach (var borrowingItem in borrowing.BorrowingItems)
        {
            borrowingItem.Book.AvailablilityNumber -= borrowingItem.Quantity;
            
            _bookService.Update(borrowingItem.Book);
        }
    }
    
    /// <summary>
    /// Adds a new borrowing transaction to the system.
    /// Ensures the validity of the transaction, updates borrowing details, and processes the borrowing.
    /// </summary>
    /// <param name="entity">
    /// The borrowing entity representing a new borrowing transaction.
    /// </param>
    public override void Add(Borrowing entity)
    {
        VerifyNumberOfAvailableBooksForBorrowing(entity);
        
        EnsureReaderExist(entity);
       
        EnsureBorrowingItemsExist(entity);
        
        entity.BorrowingDate = DateTime.Now;
       
        base.Add(entity);

        Borrow(entity);
    }

    /// <summary>
    /// Verifies that the number of returned books does not exceed the total quantity available.
    /// Throws an exception if the returned quantity exceeds the expected limit.
    /// </summary>
    /// <param name="borrowing">
    /// The borrowing transaction containing a list of borrowed items.
    /// </param>
    /// <exception cref="ServiceException">
    /// Thrown when the number of returned books exceeds the total available quantity.
    /// </exception>
    private void VerifyNumberOfReturnedBooks(Borrowing borrowing)
    {
        foreach (var borrowingItem in borrowing.BorrowingItems)
        {
            if (borrowingItem.Quantity + borrowingItem.Book.AvailablilityNumber > borrowingItem.Book.Quantity)
            {
                throw new ServiceException("There are too many returned books!");
            }
        }
    }

    /// <summary>
    /// Processes the return of borrowed books.
    /// Updates the availability count of each book and records the return date.
    /// </summary>
    /// <param name="borrowing">
    /// The borrowing transaction containing a list of items being returned.
    /// </param>
    private void ReturnBooks(Borrowing borrowing)
    {
        foreach (var borrowingItem in borrowing.BorrowingItems)
        {
            borrowingItem.Book.AvailablilityNumber += borrowingItem.Quantity;

            _bookService.Update(borrowingItem.Book);
        }
        
        borrowing.ReturningDate = DateTime.Now;
        base.Update(borrowing);
    }

    /// <summary>
    /// Handles the return process for a borrowing transaction.
    /// Ensures the validity of the returned items and updates records accordingly.
    /// </summary>
    /// <param name="borrowing">
    /// The borrowing transaction containing items that are being returned.
    /// </param>
    /// <exception cref="ServiceException">if the borrowing does not exist</exception>
    public void Return(Borrowing borrowing)
    {
        if (borrowing == null)
        {
            throw new ServiceException("Borrowing not found!");
        }
        VerifyNumberOfReturnedBooks(borrowing);
        
        ReturnBooks(borrowing);
    }

    public List<Borrowing> FilterReturnedBorrowing(bool returned = false)
    {
        return ((IBorrowingRepository) _repository).FilterReturnedBorrowing(returned);
    }

    public List<Borrowing> UnreturnedBorrowingsExceededDeadline()
    {
        return ((IBorrowingRepository) _repository).UnreturnedBorrowingsExceededDeadline();
    }

    public List<Book> GetMostBorrowedBooksCurrentYear()
    {
        return ((IBorrowingRepository) _repository).GetMostBorrowedBooksCurrentYear();
    }
}